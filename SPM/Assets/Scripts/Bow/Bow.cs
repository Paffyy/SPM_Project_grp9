using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")] 
    public GameObject Arrow;
    public GameObject Parent;
    public GameObject Player;
    public Camera playerCamera;
    public GameObject AreaOfEffectObject;
    public Text ArrowCountText;

    [Header("Variables")]
    public float MaxArrowDistance;
    public float GravityForce;
    public LayerMask AreaOfEffectMask;
    public LayerMask ArrowLayerMask;
    public int RainOfArrowCount;
    public int ArrowCount;
    public float Speed;

    private Camera bowCamera;
    private float chargeTime = 0.1f;
    private ThirdPersonCrosshair thirdPersonCrosshair;
    private Vector3 bowOffset;
    private float coolDownCounter = 0f;
    private float ArrowRainCoolDown = 10.0f;
    private bool isDoingSpecialAttack;
    private Vector3 rayPos;
    private bool foundTarget;


    void Awake()
    {
        bowOffset = new Vector3(0.55f, 0.1f, 0f);
        thirdPersonCrosshair = GetComponent<ThirdPersonCrosshair>();
        bowCamera = GetComponentInChildren<Camera>();
        Parent = Instantiate<GameObject>(Parent);
        ArrowCountText.text = ArrowCount.ToString();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownCounter <= 0 && ArrowCount > 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (thirdPersonCrosshair != null)
                {
                    thirdPersonCrosshair.ToggleCrosshair(true);
                    Player.GetComponent<Player>().FirstPersonView = true;
                }
                if (chargeTime < 2f)
                {
                    chargeTime += Time.deltaTime;
                }
                if (Input.GetKeyDown(KeyCode.E) && !CoolDownManager.Instance.ArrowRainOnCoolDown)
                {
                    isDoingSpecialAttack = !isDoingSpecialAttack;
                }
                if (isDoingSpecialAttack)
                {
                    UpdateAreaOfEffectPosition(GetRayPosition2());
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (isDoingSpecialAttack && AreaOfEffectObject.activeSelf) // special attack
                {
                    CoolDownManager.Instance.StartArrowRainCoolDown(ArrowRainCoolDown);
                    var arrowPoints = Manager.Instance.GetRandomPointsInArea(AreaOfEffectObject.transform.position, RainOfArrowCount, 
                        AreaOfEffectObject.GetComponent<SphereCollider>().radius * (
                        ((AreaOfEffectObject.transform.localScale.x + AreaOfEffectObject.transform.localScale.z)/2)));
                    foreach (var item in arrowPoints)
                    {
                        ShootArrowWithCalculatedArc(item);
                    }
                }
                else // default arrow shot
                {
                    ShootArrow();
                }
                ArrowCount--;
                ArrowCountText.text = ArrowCount.ToString();
                chargeTime = 1f; // resets
                coolDownCounter = 0.8f; // resets
                if (thirdPersonCrosshair != null)
                {
                    Player.GetComponent<Player>().FirstPersonView = false;
                    thirdPersonCrosshair.ToggleCrosshair(false);
                }
                AreaOfEffectObject.SetActive(false);
                isDoingSpecialAttack = false;
            }
        }
        else
        {
            coolDownCounter -= Time.deltaTime;
        }
        UpdatePosition();
        UpdateRotation();
    }

    public void AddArrows(int arrows)
    {
        ArrowCount += arrows;
        ArrowCountText.text = ArrowCount.ToString();
    }

    private void ShootArrow()
    {
        Vector3 direction = playerCamera.transform.forward;
        var arrow = Instantiate(Arrow, playerCamera.transform.position, Quaternion.LookRotation(direction), Parent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetGravity(GravityForce);
        arrowScript.SetDamage(chargeTime);
        arrowScript.ApplyInitialVelocity(direction * Speed);
    }
    private void ShootArrowWithCalculatedArc(Vector3 direction)
    {
        var velocity = Manager.Instance.GetInitialVelocity2(transform.position, direction, -GravityForce);
        var arrow = Instantiate(Arrow, transform.position, Quaternion.LookRotation(playerCamera.transform.forward), Parent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetGravity(GravityForce);
        arrowScript.ApplyInitialVelocity(velocity);
    }

    private void UpdateAreaOfEffectPosition(Vector3 targetPos)
    {
        if (targetPos != Vector3.zero)
        {
            AreaOfEffectObject.transform.position = targetPos;
            AreaOfEffectObject.SetActive(true);
        }
        else
        {
            AreaOfEffectObject.SetActive(false);
        }
    }
    private void UpdateRotation(float swing = 0)
    {
        var direction = playerCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * bowOffset.normalized;
        transform.position = update * bowOffset.magnitude + Player.transform.position;
    }
    private Vector3 GetRayPosition()
    {
        RaycastHit hit;
        var distance = MaxArrowDistance * (chargeTime / 1.5f);
        Physics.Raycast(bowCamera.transform.position, bowCamera.transform.forward, out hit, distance, ArrowLayerMask);
        if (hit.collider != null)
        {
            return hit.point;
        }
        else
        {
            return bowCamera.transform.position + bowCamera.transform.forward * distance;
        }
    }
    private Vector3 GetRayPosition2()
    {
        RaycastHit hit;
        var distance = MaxArrowDistance;
        Physics.Raycast(bowCamera.transform.position, bowCamera.transform.forward, out hit, distance, AreaOfEffectMask);
        if (hit.collider != null)
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}

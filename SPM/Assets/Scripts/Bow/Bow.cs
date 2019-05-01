using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    public GameObject Parent;
    public GameObject Player;
    public Camera playerCamera;
    public float MaxArrowDistance;
    public float GravityForce;
    public GameObject AreaOfEffectObject;
    public LayerMask AreaOfEffectMask;
    public LayerMask ArrowLayerMask;
    public int RainOfArrowCount;
    private Camera bowCamera;
    private float chargeTime = 0.1f;
    private ThirdPersonCrosshair thirdPersonCrosshair;
    private Vector3 bowOffset;
    private float coolDownCounter = 0f;
    private bool isDoingSpecialAttack;
    private Vector3 rayPos;
    private bool foundTarget;


    void Awake()
    {
        bowOffset = new Vector3(0.55f, 0.1f, 0f);
        thirdPersonCrosshair = GetComponent<ThirdPersonCrosshair>();
        bowCamera = GetComponentInChildren<Camera>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownCounter <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (thirdPersonCrosshair != null)
                {
                    thirdPersonCrosshair.ToggleCrosshair(true);
                }
                if (chargeTime < 1.5f)
                {
                    chargeTime += Time.deltaTime;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isDoingSpecialAttack = !isDoingSpecialAttack;
                }
                //if (speed < 25)
                //{
                //    speed += speed * Time.deltaTime;
                //}
                //if (angle < 0.4f)
                //{
                //    angle += angle * 2f * Time.deltaTime;
                //}
                if (isDoingSpecialAttack)
                {
                    UpdateAreaOfEffectPosition(GetRayPosition2());
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (isDoingSpecialAttack && AreaOfEffectObject.activeSelf) // special attack
                {
                    var arrowPoints = Manager.Instance.GetRandomPointsInArea(AreaOfEffectObject.transform.position, RainOfArrowCount, 
                        AreaOfEffectObject.GetComponent<SphereCollider>().radius * (
                        ((AreaOfEffectObject.transform.localScale.x + AreaOfEffectObject.transform.localScale.z)/2)));
                    foreach (var item in arrowPoints)
                    {
                        ShootArrow2(item);
                    }
                }
                else // default arrow shot
                {
                    ShootArrow2(GetRayPosition());
                }
                chargeTime = 0.1f;
                //speed = 10;
                //angle = 0.1f;
                coolDownCounter = 0.8f;
                if (thirdPersonCrosshair != null)
                {
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
        if (thirdPersonCrosshair != null)
        {
            thirdPersonCrosshair.PositionCrosshair();
        }
        UpdatePosition();
        UpdateRotation();
    }

    private void ShootArrow()
    {
        //var direction = playerCamera.transform.forward;
        //direction = Vector3.ProjectOnPlane(direction, Vector3.down);
        //var arrow = Instantiate(Arrow, transform.position + direction, Quaternion.LookRotation(direction * speed), Parent.transform);
        //arrow.GetComponent<Arrow>().ApplyInitialVelocity(direction.normalized * speed + new Vector3(0, angle, 0) * speed);
    }
    private void ShootArrow2(Vector3 direction)
    {
        var velocity = Manager.Instance.GetInitialVelocity2(transform.position, direction, -GravityForce);
        var arrow = Instantiate(Arrow, transform.position, Quaternion.LookRotation(playerCamera.transform.forward), Parent.transform);
        arrow.GetComponent<Arrow>().ApplyInitialVelocity(velocity);
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

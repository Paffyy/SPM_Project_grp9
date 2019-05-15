using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")]
    [SerializeField]
    private GameObject Arrow;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Camera PlayerCamera;
    [SerializeField]
    private Text ArrowCountText; // move out

    [Header("Variables")]
    [SerializeField]
    private float GravityForce; // move out
    [SerializeField]
    private LayerMask ArrowHitMask;
    [SerializeField]
    private int ArrowCount;
    [SerializeField]
    private float ArrowSpeed;

    [Header("SpecialAttacks")]
    [SerializeField]
    private SpecialArrowType SpecialAttack;
    [SerializeField]
    private int SpecialArrowCount;
    [SerializeField]
    private int SpecialAoeDamage;
    [SerializeField]
    private int SpecialAoeRadius;

    private GameObject arrowsParent;
    private float chargeTime = 0.1f;
    private ThirdPersonCrosshair crosshair;
    private Vector3 bowOffset;
    private float coolDownCounter = 0f;
    private float ArrowRainCoolDown = 10.0f;
    private bool isDoingSpecialAttack;

    [HideInInspector]
    public enum SpecialArrowType { RainOfArrows, ShotgunArrows, AoeHitArrow }

    private void Awake()
    {
        bowOffset = new Vector3(0.55f, 0.1f, 0f);
        crosshair = GetComponent<ThirdPersonCrosshair>();
        arrowsParent = Instantiate<GameObject>(new GameObject());
        ArrowCountText.text = ArrowCount.ToString();
    }
    private void OnDisable()
    {
        Player.GetComponent<Player>().FirstPersonView = false;
        crosshair.ToggleCrosshair(false);
    }
    private void OnEnable() 
    {
        Player.GetComponent<Player>().FirstPersonView = true;
        crosshair.ToggleCrosshair(true);
    }
    private void Update()
    {
        if (coolDownCounter <= 0 && ArrowCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.E) && !CoolDownManager.Instance.ArrowRainOnCoolDown)
            {
                isDoingSpecialAttack = !isDoingSpecialAttack;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (chargeTime < 2f)
                {
                    chargeTime += Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (isDoingSpecialAttack) // special attack
                {
                    CoolDownManager.Instance.StartArrowRainCoolDown(ArrowRainCoolDown);
                    switch (SpecialAttack)
                    {
                        case SpecialArrowType.RainOfArrows:
                            ShootRainOfArrows();
                            break;
                        case SpecialArrowType.ShotgunArrows:
                            ShootShotgunArrows();
                            break;
                        case SpecialArrowType.AoeHitArrow:
                            ShootAoeHitArrow();
                            break;
                        default:
                            ShootArrow();
                            break;
                    }
                }
                else // default arrow shot
                {
                    ShootArrow();
                }
                ArrowCount--;
                ArrowCountText.text = ArrowCount.ToString();
                chargeTime = 1f;
                coolDownCounter = 0.8f;
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

    private void ShootRainOfArrows()
    {
        var arrowPoints = Manager.Instance.GetRandomPointsInAreaXYZ(PlayerCamera.transform.forward, 50, SpecialArrowCount, 2);
        foreach (var item in arrowPoints)
        {
            ShootArrowWithCalculatedArc(item);
        }
    }
    private void ShootShotgunArrows()
    {
        var arrowPoints = Manager.Instance.GetRandomPointsInAreaXYZ(PlayerCamera.transform.forward, 50, SpecialArrowCount, 2);
        foreach (var item in arrowPoints)
        {
            ShootArrowShotgun(item.normalized);
        }
    }
    private void ShootAoeHitArrow()
    {
        ShootArrowExplosion(PlayerCamera.transform.forward);
    }
    private void ShootArrow()
    {
        Vector3 direction = PlayerCamera.transform.forward;
        var arrow = Instantiate(Arrow, PlayerCamera.transform.position, Quaternion.LookRotation(direction), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        SetArrowProperties(arrowScript, direction * ArrowSpeed, chargeTime);
    }
    // Rain of arrow 
    private void ShootArrowWithCalculatedArc(Vector3 direction)
    {
        float gravityModifier = 2.4f; // Only parameter to alter time to impact
        var velocity = Manager.Instance.GetInitialVelocity2(transform.position, direction, -GravityForce * gravityModifier);
        var arrow = Instantiate(Arrow, transform.position, Quaternion.LookRotation(PlayerCamera.transform.forward), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        SetArrowProperties(arrowScript, velocity, 1);
        arrowScript.SetGravity(GravityForce * gravityModifier);
    }
    // Shoots an array of arrows where aiming
    private void ShootArrowShotgun(Vector3 direction)
    {
        var arrow = Instantiate(Arrow, PlayerCamera.transform.position, Quaternion.LookRotation(PlayerCamera.transform.forward), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        SetArrowProperties(arrowScript, direction * ArrowSpeed, 1);
    }
    // aoe around the arrow hit
    private void ShootArrowExplosion(Vector3 direction)
    {
        var arrow = Instantiate(Arrow, PlayerCamera.transform.position, Quaternion.LookRotation(PlayerCamera.transform.forward), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        SetArrowProperties(arrowScript, direction * ArrowSpeed, chargeTime);
        arrowScript.EnableAoeOnHit(SpecialAoeDamage, SpecialAoeRadius);
    }

    private void SetArrowProperties(Arrow arrow , Vector3 initialVelocity, float damageMultiplier)
    {
        arrow.SetGravity(GravityForce);
        arrow.SetDamage(damageMultiplier);
        arrow.ApplyInitialVelocity(initialVelocity);
    }

    private void UpdateRotation(float swing = 0)
    {
        var direction = PlayerCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * bowOffset.normalized;
        transform.position = update * bowOffset.magnitude + Player.transform.position;
    }
}

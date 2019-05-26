using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject Arrow;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Camera PlayerCamera;
    [SerializeField]
    private Text ArrowCountText; // TODO move out

    [Header("Variables")]
    [SerializeField]
    private float GravityForce; // TODO move out
    [SerializeField]
    private LayerMask ArrowHitMask;
    [SerializeField]
    private int arrowCount;
    [SerializeField]
    private float arrowSpeed;

    [Header("SpecialAttacks")]
    [SerializeField]
    private SpecialArrowType SpecialAttack;
    [SerializeField]
    private int SpecialArrowCount;
    [SerializeField]
    private int SpecialAoeDamage;
    [SerializeField]
    private int SpecialAoeRadius;
    [SerializeField]
    private Vector3 bowOffset;
    [SerializeField]
    private GameObject specialAttackGlow;

    [SerializeField]
    private Weapon weapon;
    private GameObject arrowsParent;
    private float chargeTime = 1;
    private ThirdPersonCrosshair crosshair;
    private float coolDownCounter = 0f;
    private bool isDoingSpecialAttack;
    private Animator animator;
    private Player playerScript;
    [HideInInspector]
    public int ArrowCount { get { return arrowCount; } set { arrowCount = value; ArrowCountText.text = arrowCount.ToString(); } }
    public enum SpecialArrowType { RainOfArrows, ShotgunArrows, AoeHitArrow }

    private void Awake()
    {
        crosshair = GetComponent<ThirdPersonCrosshair>();
        arrowsParent = new GameObject("ArrowContainer");
        ArrowCountText.text = weapon.ArrowCount.ToString();
        //playerScript = Player.GetComponent<Player>();
        chargeTime = Mathf.Clamp(chargeTime, 1, 2);
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        ToggleCrosshair(false);
        specialAttackGlow.SetActive(false);
    }

    private void ToggleCrosshair(bool toggle)
    {
        playerScript.FirstPersonView = toggle;
        crosshair.ToggleCrosshair(toggle);
    }

    private void Update()
    {
        ToggleCrosshair(true);
        if (coolDownCounter <= 0 && weapon.ArrowCount > 0)
        {
            if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.SpecialAttack, InputManager.ControllMode.Play) && !CoolDownManager.Instance.ArrowRainOnCoolDown)
                //if (Input.GetKeyDown(KeyCode.E))
            {
                isDoingSpecialAttack = !isDoingSpecialAttack;
                specialAttackGlow.SetActive(!specialAttackGlow.activeSelf);

            }
            if (InputManager.Instance.Getkey(KeybindManager.Instance.ShootAndAttack, InputManager.ControllMode.Play))
            //if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetBool("IsChargingBow", true);
                if (chargeTime < 2f)
                {
                    chargeTime += Time.deltaTime;
                }
            }

            if (InputManager.Instance.GetkeyUp(KeybindManager.Instance.ShootAndAttack, InputManager.ControllMode.Play))
                //if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (isDoingSpecialAttack) // special attack
                {
                    CoolDownManager.Instance.StartArrowRainCoolDown(10);
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
                weapon.ArrowCount--;
                ArrowCountText.text = weapon.ArrowCount.ToString();
                ResetBow();
            }
        }
        else
        {
            coolDownCounter -= Time.deltaTime;
        }
        //UpdateRotation();
        UpdatePosition();
    }
    private void ResetBow()
    {
        chargeTime = 1f;
        coolDownCounter = 0.8f;
        isDoingSpecialAttack = false;
        animator.SetBool("IsChargingBow", false);
        specialAttackGlow.SetActive(false);
    }

    public void AddArrows(int arrows)
    {
        weapon.ArrowCount += arrows;
        ArrowCountText.text = weapon.ArrowCount.ToString();
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
        float speed = arrowSpeed * chargeTime;
        SetArrowProperties(arrowScript, direction * speed, chargeTime);
    }
    // Rain of arrow 
    private void ShootArrowWithCalculatedArc(Vector3 direction)
    {
        float gravityModifier = 2.4f; // Only parameter to alter time to impact with calculated arc
        Vector3 velocity = Manager.Instance.GetInitialVelocity2(transform.position, direction, -GravityForce * gravityModifier);
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
        SetArrowProperties(arrowScript, direction * arrowSpeed, 1);
    }
    // aoe around the arrow hit
    private void ShootArrowExplosion(Vector3 direction)
    {
        var arrow = Instantiate(Arrow, PlayerCamera.transform.position, Quaternion.LookRotation(PlayerCamera.transform.forward), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        float speed = arrowSpeed * chargeTime;
        SetArrowProperties(arrowScript, direction * speed, chargeTime);
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

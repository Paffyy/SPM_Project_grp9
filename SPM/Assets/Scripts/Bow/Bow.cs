using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bow : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private AudioClip soundClip;

    [Header("Variables")]
    [SerializeField]
    private float gravityForce; // TODO move out
    [SerializeField]
    private LayerMask arrowHitMask;
    [SerializeField]
    private float arrowSpeed;
    [SerializeField]
    private Vector3 bowOffset;

    [Header("SpecialAttacks")]
    [SerializeField]
    private SpecialArrowType specialAttack;
    [SerializeField]
    private int specialArrowCount;
    [SerializeField]
    private int specialAoeDamage;
    [SerializeField]
    private int specialAoeRadius;
    [SerializeField]
    private GameObject specialAttackGlow;
  
    [SerializeField]
    private Weapon weapon;
    private GameObject arrowsParent;
    private float chargeTime = 2; // deprecated
    private ThirdPersonCrosshair crosshair;
    private float coolDownCounter = 0f;
    private bool isDoingSpecialAttack;
    private Animator animator;
    private Player playerScript;
    private AudioSource audioSource;
    public enum SpecialArrowType { RainOfArrows, ShotgunArrows, AoeHitArrow }
    private void Awake()
    {
        crosshair = GetComponent<ThirdPersonCrosshair>();
        arrowsParent = new GameObject("ArrowContainer");
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsChargingBow", true);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundClip;
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
        if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.SpecialAttack, InputManager.ControllMode.Play) && !CoolDownManager.Instance.ArrowRainOnCoolDown)
        {
            isDoingSpecialAttack = !isDoingSpecialAttack;
            specialAttackGlow.SetActive(!specialAttackGlow.activeSelf);
        }
        if (coolDownCounter <= 0 && weapon.ArrowCount > 0)
        {
            if (InputManager.Instance.GetkeyUp(KeybindManager.Instance.ShootAndAttack, InputManager.ControllMode.Play))
            {
                PlaySoundEffect();
                animator.SetBool("IsChargingBow", false);
                if (isDoingSpecialAttack) // special attack
                {
                    CoolDownManager.Instance.StartArrowRainCoolDown(10);
                    switch (specialAttack)
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
                else
                {
                    ShootArrow();
                }
                weapon.ArrowCount--;
                ResetBow();
            }
        }
        else
        {
            coolDownCounter -= Time.deltaTime;
            animator.SetBool("IsChargingBow", true);
        }
        UpdateRotation();
        UpdatePosition();
    }

 
    private void ResetBow()
    {
        coolDownCounter = 0.5f;
        isDoingSpecialAttack = false;
        specialAttackGlow.SetActive(false);
    }

    private void PlaySoundEffect()
    {
        if(audioSource != null && soundClip != null)
        {
            audioSource.Play();
        }
    }

    public void AddArrows(int arrows)
    {
        weapon.ArrowCount += arrows;
        //ArrowCountText.text = weapon.ArrowCount.ToString();
    }
    private void ShootRainOfArrows()
    {
        var arrowPoints = Manager.Instance.GetRandomPointsInAreaXYZ(playerCamera.transform.forward, 50, specialArrowCount, 2);
        foreach (var item in arrowPoints)
        {
            ShootArrowWithCalculatedArc(item);
        }
    }
    private void ShootShotgunArrows()
    {
        var arrowPoints = Manager.Instance.GetRandomPointsInAreaXYZ(playerCamera.transform.forward, 50, specialArrowCount, 2);
        foreach (var item in arrowPoints)
        {
            ShootArrowShotgun(item.normalized);
        }
    }
    private void ShootAoeHitArrow()
    {
        ShootArrowExplosion(playerCamera.transform.forward);
    }

    private void ShootArrow()
    {
        Vector3 direction = playerCamera.transform.forward;
        var arrow = Instantiate(this.arrow, playerCamera.transform.position + direction * arrowSpeed / 25f, Quaternion.LookRotation(direction), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        float speed = arrowSpeed * chargeTime;
        SetArrowProperties(arrowScript, direction * speed, chargeTime);
    }
    // Rain of arrow 
    private void ShootArrowWithCalculatedArc(Vector3 direction)
    {
        float gravityModifier = 2.4f; // Only parameter to alter time to impact with calculated arc
        Vector3 velocity = Manager.Instance.GetInitialVelocity2(transform.position, direction, -gravityForce * gravityModifier);
        var arrow = Instantiate(this.arrow, transform.position, Quaternion.LookRotation(playerCamera.transform.forward), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        SetArrowProperties(arrowScript, velocity, 1);
        arrowScript.SetGravity(gravityForce * gravityModifier);
    }
    // Shoots an array of arrows where aiming
    private void ShootArrowShotgun(Vector3 direction)
    {
        var arrow = Instantiate(this.arrow, playerCamera.transform.position, Quaternion.LookRotation(playerCamera.transform.forward), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        SetArrowProperties(arrowScript, direction * arrowSpeed, 1);
    }
    // aoe around the arrow hit
    private void ShootArrowExplosion(Vector3 direction)
    {
        var arrow = Instantiate(this.arrow, playerCamera.transform.position, Quaternion.LookRotation(playerCamera.transform.forward), arrowsParent.transform);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        float speed = arrowSpeed * chargeTime;
        SetArrowProperties(arrowScript, direction * speed, chargeTime);
        arrowScript.EnableAoeOnHit(specialAoeDamage, specialAoeRadius);
    }

    private void SetArrowProperties(Arrow arrow, Vector3 initialVelocity, float damageMultiplier)
    {
        arrow.SetGravity(gravityForce);
        arrow.SetDamage(damageMultiplier);
        arrow.ApplyInitialVelocity(initialVelocity);
    }

    private void UpdateRotation(float swing = 0)
    {
        var direction = playerCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * bowOffset.normalized;
        transform.position = update * bowOffset.magnitude + player.transform.position;
    }
}

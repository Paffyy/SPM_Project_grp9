using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Sword : MonoBehaviour
{
    public bool IsBladeStorming { get; set; }
    [SerializeField]
    private float radius;
    [Range(0, 360)] [SerializeField]
    private float angle;
    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    private float coolDownValue;
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private int damage = 50;
    [SerializeField]
    private int bladeStormDamage = 20;
    [SerializeField]
    private GameObject bladeStormEffect;
    private Animator swordAnimator;
    [SerializeField]
    private ParticleSystem trails;
    private float coolDownCounter;
    private bool onCooldown;
    private Vector3 swordOffset;
    private float swingValue = 70f;
    private float bladeStormCoolDown = 10.0f;
    private float BladeStormTimer = 3f;
    private ParticleSystem.EmissionModule trailsEmissionModule;
    private bool isAttacking;
    [SerializeField]
    private AudioClip attackSoundClip;
    [SerializeField]
    private AudioClip bladeStormSoundClip;
    private Player playerScript;
    private AudioSource audioSource;

    void Start()
    {
        swordAnimator = GetComponent<Animator>();
        playerScript = playerObject.GetComponent<Player>();
        swordOffset = new Vector3(0.3f, 0.2f, 0.55f);
        trailsEmissionModule = trails.emission;
        isAttacking = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attackSoundClip;
    }

    void Update()
    {
        if (Manager.Instance.IsPaused == false && InputManager.Instance.GetkeyDown(KeybindManager.Instance.SpecialAttack, InputManager.ControllMode.Play) && !CoolDownManager.Instance.BladeStormOnCoolDown && !IsBladeStorming && !isAttacking && !playerObject.GetComponent<Weapon>().Shield.GetComponent<Shield>().IsBlocking)
        {
            BladeStorm();
            playerScript.CharacterAnimator.SetTrigger("Spin");
            IsBladeStorming = true;
            CoolDownManager.Instance.StartBladeStormCoolDown(bladeStormCoolDown);
        }
        if (IsBladeStorming)
        {
            if (bladeStormEffect.activeInHierarchy == false)
            {
                bladeStormEffect.SetActive(true);
                bladeStormEffect.transform.position = playerObject.transform.position;
            }
            var direction = playerCamera.transform.forward;
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90 + swingValue, 0, 0);
            BladeStormTimer -= Time.deltaTime;
            if (BladeStormTimer <= 0)
            {
                bladeStormEffect.SetActive(false);
                IsBladeStorming = false;
                BladeStormTimer = 3f;
                StopAllCoroutines();
            }
        }
        if (!IsBladeStorming)
        {
            if (coolDownCounter < 0)
            {
                if (Manager.Instance.IsPaused == false && InputManager.Instance.GetkeyDown(KeybindManager.Instance.ShootAndAttack, InputManager.ControllMode.Play) && !isAttacking && !playerObject.GetComponent<Weapon>().Shield.GetComponent<Shield>().IsBlocking)
                {
                    coolDownCounter = coolDownValue;
                    playerScript.CharacterAnimator.SetTrigger("Sword");
                    Attack();
                }
                UpdateRotation();
            }
            else
            {
                coolDownCounter -= Time.deltaTime;

                if (coolDownCounter < coolDownValue)
                {
                    UpdateRotation(swingValue);
                }
                else
                {
                    UpdateRotation(swingValue);
                }
            }
        }
        UpdatePosition();
    }

    IEnumerator InflictBladeStormDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            List<Collider> colliders = Manager.Instance.GetAoeHit(playerObject.transform.position, collisionMask, 4.8f);
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Enemy"))
                {
                    EventHandler.Instance.FireEvent(EventHandler.EventType.WeapondHitEvent, new AttackHitEventInfo(playerObject.transform, c, AttackHitEventInfo.Weapon.Sword));
                    c.gameObject.GetComponent<EnemyHealth>().TakeDamage(bladeStormDamage, true);
                }
            }
        }
    }

    void Attack()
    {
        isAttacking = true;
    }

    public void SetIsAttacking()
    {
        isAttacking = false;
    }

    public void SpawnTrail()
    {
        trailsEmissionModule.enabled = true;
    }

    public void StopTrail()
    {
        trailsEmissionModule.enabled = false;
    }

    private void BladeStorm()
    {
        PlaySoundEffect(bladeStormSoundClip, true);
        StartCoroutine(InflictBladeStormDamage());
    }

    private void UpdateRotation(float swing = 0)
    {
        //Vector3 direction = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up);
        //transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-15, 90, 0);
    }

    private void UpdatePosition()
    {
        //Vector3 update = transform.rotation * swordOffset.normalized;
        //transform.position = update * swordOffset.magnitude + playerObject.transform.position;
    }

    void CheckCollision()
    {
        var enemyInRange = Manager.Instance.GetFrontConeHit(playerCamera.transform.forward, playerObject.transform, collisionMask, radius, angle);
        foreach (var item in enemyInRange)
        {
            EventHandler.Instance.FireEvent(EventHandler.EventType.WeapondHitEvent, new AttackHitEventInfo(playerObject.transform ,item, AttackHitEventInfo.Weapon.Sword));
            DealDamage(item);
            PlaySoundEffect(attackSoundClip);
        }
    }

    private void DealDamage(Collider item)
    {
        Vector3 pushBack = (Vector3.ProjectOnPlane((item.gameObject.transform.position - playerObject.transform.position), Vector3.up).normalized + Vector3.up * 5) * 2;
        item.gameObject.GetComponent<Health>().TakeDamage(damage, pushBack, playerObject.transform.position);
        Color c = item.GetComponent<Renderer>().material.color;
        item.GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(RemoveRedColor(item, c));
    }
    private void PlaySoundEffect(AudioClip clip, bool overrideSound = false)
    {
        if (audioSource != null && clip != null)
        {
            if (!audioSource.isPlaying || overrideSound)
	        {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
    //testing
    IEnumerator RemoveRedColor(Collider item, Color c)
    {
        yield return new WaitForSeconds(0.2f);
        if (item != null)
            item.GetComponent<Renderer>().material.color = c;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    public float Radius;
    [Range(0, 360)]
    public float Angle;
    public LayerMask CollisionMask;
    public float CoolDownValue;
    public GameObject PlayerObject;
    public Camera playerCamera;
    public int Damage = 50;
    public int BladeStormDamage = 5;
    public GameObject BladeStormEffect;
    public bool IsBladeStorming;
    public Animator Anim;
    public ParticleSystem Trails;
    public GameObject SlashParticleSystem;
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
        playerScript = PlayerObject.GetComponent<Player>();
        swordOffset = new Vector3(0.3f, 0.2f, 0.55f);
        trailsEmissionModule = Trails.emission;
        isAttacking = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attackSoundClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.Instance.IsPaused == false && Input.GetKeyDown(KeyCode.E) && !CoolDownManager.Instance.BladeStormOnCoolDown && !IsBladeStorming && !isAttacking && !PlayerObject.GetComponent<Weapon>().Shield.GetComponent<Shield>().IsBlocking)
        {
            BladeStorm();
            playerScript.characterAnimator.SetTrigger("Spin");
            IsBladeStorming = true;
            CoolDownManager.Instance.StartBladeStormCoolDown(bladeStormCoolDown);
        }
        if (IsBladeStorming)
        {
            if (BladeStormEffect.activeInHierarchy == false)
            {
                BladeStormEffect.SetActive(true);
                BladeStormEffect.transform.position = PlayerObject.transform.position;
            }
            var direction = playerCamera.transform.forward;
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90 + swingValue, 0, 0);
            BladeStormTimer -= Time.deltaTime;
            if (BladeStormTimer <= 0)
            {
                BladeStormEffect.SetActive(false);
                IsBladeStorming = false;
                BladeStormTimer = 3f;
                StopAllCoroutines();
            }
        }
        if (!IsBladeStorming)
        {
            if (coolDownCounter < 0)
            {
                if (Manager.Instance.IsPaused == false && Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && !PlayerObject.GetComponent<Weapon>().Shield.GetComponent<Shield>().IsBlocking)
                {
                    coolDownCounter = CoolDownValue;
                    playerScript.characterAnimator.SetTrigger("Sword");
                    Attack();
                }
                UpdateRotation();
            }
            else
            {
                coolDownCounter -= Time.deltaTime;

                if (coolDownCounter < CoolDownValue)
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
            List<Collider> colliders = Manager.Instance.GetAoeHit(PlayerObject.transform.position, CollisionMask, 4.8f);
            // Gizmos.DrawSphere(PlayerObject.transform.position, BladeStormCollider.radius * ((transform.localScale.x + transform.localScale.z) / 2));
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Enemy"))
                {
                    EventHandler.Instance.FireEvent(EventHandler.EventType.WeapondHitEvent, new AttackHitEventInfo(PlayerObject.transform, c, AttackHitEventInfo.Weapon.Sword));
                    c.gameObject.GetComponent<EnemyHealth>().TakeDamage(BladeStormDamage, true);
                }
            }
        }
    }


    void Attack()
    {
        Anim.SetBool("Attack", true);
        isAttacking = true;
    }

    public void EnableSlashEffect()
    {
        //GameObject PSClone = Instantiate(SlashParticleSystem, transform.position, Quaternion.identity, transform);
        //Destroy(PSClone, 0.4f);
    }

    public void DisableSlashEffect()
    {
    }

    public void GoToIdle()
    {
        Anim.SetBool("Attack", false);
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
        //var direction = playerCamera.transform.forward;
        Vector3 direction = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up);
        //  transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90 + swing, 0, 0);
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-15, 90, 0);
    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * swordOffset.normalized;
        transform.position = update * swordOffset.magnitude + PlayerObject.transform.position;
    }

    void CheckCollision()
    {
        var enemyInRange = Manager.Instance.GetFrontConeHit(playerCamera.transform.forward, PlayerObject.transform, CollisionMask, Radius, Angle);
        foreach (var item in enemyInRange)
        {
            EventHandler.Instance.FireEvent(EventHandler.EventType.WeapondHitEvent, new AttackHitEventInfo(PlayerObject.transform ,item, AttackHitEventInfo.Weapon.Sword));
            DealDamage(item);
            PlaySoundEffect(attackSoundClip);
        }
    }

    private void DealDamage(Collider item)
    {
        Vector3 pushBack = (Vector3.ProjectOnPlane((item.gameObject.transform.position - PlayerObject.transform.position), Vector3.up).normalized + Vector3.up * 5) * 2;
        item.gameObject.GetComponent<Health>().TakeDamage(Damage, pushBack, PlayerObject.transform.position);
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

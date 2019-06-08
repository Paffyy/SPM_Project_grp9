using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private CapsuleCollider capCollider;
    private float gravityForce;
    private int limit;
    private Vector3 velocity;
    private bool hasCollided;
    private bool isTerminating;
    private int damage;

    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    private int baseDamage;
    [SerializeField]
    private int aoeDamage;
    [SerializeField]
    private int aoeRadius;
    [SerializeField]
    private AudioClip soundClip;

    private AudioSource audioSource;

    public int AoeDamage
    {
        get { return aoeDamage; }
        set { aoeDamage = value; }
    }

    public int AoeRadius
    {
        get { return aoeRadius; }
        set { aoeRadius = value; }
    }
    public bool IsAoeHitEnabled { get; private set; }

    private void Awake()
    {
        capCollider = GetComponentInChildren<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundClip;
    }
    private void Update()
    {
        if (!hasCollided)
        {
            ApplyGravity();
            transform.rotation = Quaternion.LookRotation(velocity);
            IsColliding();
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            if (!isTerminating)
            {
                PlaySoundEffect();
                StartCoroutine(Terminate());
            }
        }
    }

    private IEnumerator Terminate()
    {
        isTerminating = true;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void PlaySoundEffect()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.Play();
        }
    }
    public void SetGravity(float gravForce)
    {
        gravityForce = gravForce;
    }
    public void SetDamage(float chargeTime)
    {
        damage = (int)(baseDamage * chargeTime);
    }
    public void ApplyInitialVelocity(Vector3 v)
    {
        velocity += v;
    }

    public void EnableAoeOnHit(int aoeDamage, int radius)
    {
        IsAoeHitEnabled = true;
        AoeDamage = aoeDamage;
        AoeRadius = radius;
    }

    protected virtual void ApplyGravity()
    {
        velocity += Vector3.down * gravityForce * Time.deltaTime;
    }

    protected virtual void IsColliding()
    {
        RaycastHit hit;
        Vector3 point1 = transform.position + capCollider.center + velocity.normalized * (capCollider.height / 2 - capCollider.radius);
        Vector3 point2 = transform.position + capCollider.center + -velocity.normalized * (capCollider.height / 2 - capCollider.radius);
        if (Physics.Raycast(transform.position, velocity.normalized, out hit, velocity.magnitude * Time.deltaTime, collisionMask))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                audioSource.volume = 0.05f;
                Vector3 pushBack = Vector3.up * 13;
                gameObject.transform.SetParent(hit.collider.gameObject.transform);
                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, pushBack, Vector3.zero);
                EventHandler.Instance.FireEvent(EventHandler.EventType.WeapondHitEvent, new AttackHitEventInfo(null, hit.collider, AttackHitEventInfo.Weapon.Bow));
            }
            audioSource.volume = 0.03f;
            hasCollided = true;
            if (EventHandler.Instance != null)
            {
                var e = new ArrowHitEventInfo(gameObject);
                EventHandler.Instance.FireEvent(EventHandler.EventType.RevitalizeEvent, e);
                if (IsAoeHitEnabled)
                {
                    EventHandler.Instance.FireEvent(EventHandler.EventType.ArrowAoeHitEvent, e);
                }
            }

        }
    }

}

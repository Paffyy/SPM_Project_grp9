using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public LayerMask CollisionMask;
    public int Damage;

    private int baseDamage = 25;
    private CapsuleCollider capCollider;
    private float gravityForce;
    private int limit;
    private Vector3 velocity;
    private bool hasCollided;
    private bool isTerminating;

    [HideInInspector]
    public int AoeDamage;
    public int AoeRadius;
    public bool IsAoeHitEnabled;

    private void Awake()
    {
        capCollider = GetComponentInChildren<CapsuleCollider>();
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
    public void SetGravity(float gravForce)
    {
        gravityForce = gravForce;
    }
    public void SetDamage(float chargeTime)
    {
        Damage = (int)(baseDamage * chargeTime);
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
        if (Physics.Raycast(transform.position, velocity.normalized, out hit, velocity.magnitude * Time.deltaTime, CollisionMask))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                //TODO fixa
                //transform.forward är inte den riktningen som pilen färdas i
                Vector3 pushBack = Vector3.ProjectOnPlane(transform.forward, Vector3.up) * 2 + (Vector3.up * 2) * 3;
                gameObject.transform.SetParent(hit.collider.gameObject.transform);
                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage, pushBack, Vector3.zero);
            }
            //if (hit.collider.gameObject.CompareTag("RevObject"))
            //{
            //    hit.collider.gameObject.GetComponent<RevitalizeGeometry>().Revitalize();
            //}
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

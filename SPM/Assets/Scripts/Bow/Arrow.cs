using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public LayerMask CollisionMask;
    public float GravityForce;
    private CapsuleCollider capCollider;
    private int limit;
    private float skinWidth;
    private Vector3 velocity;
    private bool hasCollided;
    private bool isTerminating;
    void Awake()
    {
        skinWidth = 0.02f;
        capCollider = GetComponentInChildren<CapsuleCollider>();
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!hasCollided)
        {
            ApplyGravity();
            transform.rotation = Quaternion.LookRotation(velocity);
            transform.position += velocity * Time.deltaTime;
            IsColliding();
        }
        else
        {
            if (!isTerminating)
            {
                StartCoroutine(Terminate());
            }
        }
    }

    IEnumerator Terminate()
    {
        isTerminating = true;
        yield return new WaitForSeconds(5);
        Destroy(this);
    }
    public void ApplyInitialVelocity(Vector3 v)
    {
        velocity += v;
    }
    protected virtual void ApplyGravity()
    {
        velocity += Vector3.down * GravityForce * Time.deltaTime;
    }

    protected virtual void IsColliding()
    {
        RaycastHit hit;
        Vector3 point1 = transform.position + capCollider.center + Vector3.up * (capCollider.height / 2 - capCollider.radius);
        Vector3 point2 = transform.position + capCollider.center + Vector3.down * (capCollider.height / 2 - capCollider.radius);
        if (Physics.CapsuleCast(point1, point2, capCollider.radius, velocity.normalized, out hit, velocity.magnitude * Time.deltaTime + skinWidth, CollisionMask) && limit < 10)
        {
            RaycastHit normalHit;
            Physics.CapsuleCast(point1, point2, capCollider.radius, -hit.normal, out normalHit, velocity.magnitude * Time.deltaTime + skinWidth, CollisionMask);
            Vector3 moveDistance = velocity.normalized * (normalHit.distance - skinWidth);
            transform.position += moveDistance;
            limit++;
            IsColliding();
            hasCollided = true;
        }
        limit = 0;
    }
}

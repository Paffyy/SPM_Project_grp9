using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    public LayerMask CollisionMask;
    [HideInInspector] public Vector3 Velocity;


    [SerializeField] private float gravityForce = 30.0f;
    [SerializeField] private float Acceleration = 20f;
    [SerializeField] private float skinWidth = 0.005f;
    [SerializeField] private float groundCheckDistance = 0.5f;
    private int collisionLimit = 10;
    private float staticFriction = 0.9f;
    private float dynamicFriction = 0.6f;
    [SerializeField] private float maxClimbAngle = 60;
    private CapsuleCollider characterCollider;

    void Start()
    {

        characterCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        IsColliding();
        transform.position += Velocity * Time.deltaTime;
    }

    private void ApplyGravity()
    {
        Vector3 gravity = Vector3.down * gravityForce * Time.deltaTime;
        Velocity += gravity;
    }

    private void IsColliding()
    {
        RaycastHit hit;
        Vector3 point1 = transform.position + characterCollider.center + Vector3.up * (characterCollider.height / 2 - characterCollider.radius);
        Vector3 point2 = transform.position + characterCollider.center + Vector3.down * (characterCollider.height / 2 - characterCollider.radius);
        int collisionCount = 0;
        if (Physics.CapsuleCast(point1, point2, characterCollider.radius, Velocity.normalized, out hit, Velocity.magnitude * Time.deltaTime + skinWidth, CollisionMask) && collisionCount < collisionLimit)
        {
            RaycastHit normalHit;
            Physics.CapsuleCast(point1, point2, characterCollider.radius, -hit.normal, out normalHit, Velocity.magnitude * Time.deltaTime + skinWidth, CollisionMask);
            Vector3 moveDistance = Velocity.normalized * (normalHit.distance - skinWidth);
            transform.position += moveDistance;
            Vector3 projection = GetProjection(Velocity, hit.normal);
            Velocity += projection;
            ApplyFriction(projection.magnitude);
            collisionCount++;
            IsColliding();
        }
        collisionCount = 0;
    }

    private void ApplyFriction(float normalForce)
    {
        if (Velocity.magnitude < (normalForce * staticFriction))
        {
            Velocity = Vector3.zero;
        }
        else
        {
            Velocity += -Velocity.normalized * (normalForce * dynamicFriction);
        }
    }

    private Vector3 GetProjection(Vector3 velocity, Vector3 normal)
    {
        float x = Vector3.Dot(velocity, normal);
        Vector3 projection;
        if (x > 0)
        {
            projection = 0 * normal;
        }
        else
        {
            projection = x * normal;
        }
        return -projection;
    }


    public bool IsGrounded()
    {
        RaycastHit hit;
        Vector3 point1 = transform.position + characterCollider.center + Vector3.up * (characterCollider.height / 2 - characterCollider.radius);
        Vector3 point2 = transform.position + characterCollider.center + Vector3.down * (characterCollider.height / 2 - characterCollider.radius);
        if (Physics.CapsuleCast(point1, point2, characterCollider.radius, Vector3.down, out hit, groundCheckDistance + skinWidth, CollisionMask))
        {
            float angle = Vector3.Angle(Vector3.up, hit.normal);

            if (angle > maxClimbAngle)
            {
                return false;
            }

            return true;
        }
        return false;
    }

}

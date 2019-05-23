using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpelCharacterController : MonoBehaviour
{
    public LayerMask CollisionMask;
    [HideInInspector] public Vector3 Velocity;
    [SerializeField] private GameObject groundedObject;


    public float GravityForce = 30.0f;
    
    public float JumpDistance = 12;
    public float Acceleration = 20f;


    public float skinWidth = 0.005f;
    public float groundCheckDistance = 0.5f;
    private CapsuleCollider characterCollider;
    private int collisionLimit = 10;

    public float StaticFriction = 0.9f;
    public float DynamicFriction = 0.6f;

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

    public void MovePosition(Vector3 newPos)
    {
        Vector3 direction = newPos;
        //direction = Vector3.ProjectOnPlane(direction, GetGroundNormal().normalized);
        float distance = Acceleration * Time.deltaTime;
        Velocity += direction.normalized * distance;

    }

    //TODO rotationen funkar inte som den ska
    //public void MoveRotation(Vector3 targetDir, float speed)
    //{
    //    rotateDir = targetDir;

    //}

    private void ApplyGravity()
    {
        Vector3 gravity = Vector3.down * GravityForce * Time.deltaTime;
        Velocity += gravity;
    }

    private void IsColliding()
    {
        RaycastHit hit;
        Vector3 point1 = transform.position + characterCollider.center + Vector3.up * (characterCollider.height / 2 - characterCollider.radius);
        Vector3 point2 = transform.position + characterCollider.center + Vector3.down * (characterCollider.height / 2 - characterCollider.radius);
        if (Physics.CapsuleCast(point1, point2, characterCollider.radius, Velocity.normalized, out hit, Velocity.magnitude * Time.deltaTime + skinWidth, CollisionMask) && collisionLimit < 10)
        {
            RaycastHit normalHit;
            Physics.CapsuleCast(point1, point2, characterCollider.radius, -hit.normal, out normalHit, Velocity.magnitude * Time.deltaTime + skinWidth, CollisionMask);
            Vector3 moveDistance = Velocity.normalized * (normalHit.distance - skinWidth);
            transform.position += moveDistance;
            Vector3 projection = GetProjection(Velocity, hit.normal);
            Velocity += projection;
            ApplyFriction(projection.magnitude);
            collisionLimit++;
            IsColliding();
        }
        collisionLimit = 0;
    }

    private void ApplyFriction(float normalForce)
    {
        if (Velocity.magnitude < (normalForce * StaticFriction))
        {
            Velocity = Vector3.zero;
        }
        else
        {
            Velocity += -Velocity.normalized * (normalForce * DynamicFriction);
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

            if (angle > 60)
            {
                return false;
            }

            return true;
        }
        return false;
    }


    //funkar inte riktigt kan kanske ta bort hela skiten!
    public bool AdvancedIsGrounded()
    {
        RaycastHit hit;
        Vector3 helper = Vector3.zero;
        if(Velocity == Vector3.zero)
        {
            Debug.Log("bopo");
            helper = Vector3.down;
            //Debug.Log("vel " + (Velocity.normalized + helper) * groundCheckDistance + skinWidth).magnidute));
        }
        Debug.DrawRay(groundedObject.transform.position, (Velocity.normalized + helper) * (groundCheckDistance + skinWidth), Color.green);
        if (Physics.SphereCast(groundedObject.transform.position, characterCollider.radius, Velocity.normalized, out hit, groundCheckDistance, CollisionMask))
        {
            Debug.DrawRay(groundedObject.transform.position, hit.normal, Color.green);
            float angle = Vector3.Angle(Vector3.up, hit.normal);

            if(angle > 60)
            {
                return false;
            }
            
            return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public LayerMask CollisionMask;
    [HideInInspector]public Vector3 Velocity;
    private float RotationX;
    private float RotationY;
    private Quaternion Rotation;
    private Vector3 rotateDir;
    public float RotationSpeed;

    public float Acceleration = 40.0f;
    public float GravityForce = 30.0f;
    public float StaticFriction = 0.9f;
    public float DynamicFriction = 0.6f;
    public float JumpDistance = 12;
    public float AirResistance = 0.3f;
    public float MouseSensitivity = 2.0f;
    public float skinWidth = 0.005f;
    public float groundCheckDistance = 0.5f;
    private CapsuleCollider characterCollider;
    private int collisionLimit = 0;


    //private Vector3 Position { get { return transform.position; } set { transform.position = value; } }

    // Start is called before the first frame update
    void Awake()
    {

        characterCollider = GetComponent<CapsuleCollider>();

    }

    private void Update()
    {
        ApplyGravity();
        IsColliding();
        Velocity *= Mathf.Pow(AirResistance, Time.deltaTime);
        transform.position += Velocity * Time.deltaTime;


        //TODO rotationen funkar inte som den ska
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, rotateDir, Time.deltaTime * RotationSpeed, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDir);
        //float targetRotation = Mathf.Atan2(rotateDir.x, rotateDir.y) * Mathf.Rad2Deg + rotateDir.y;
        //transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVel, turnSmoothTime);
        //rotateDir = transform.eulerAngles;
    }

    public void MovePosition(Vector3 newPos)
    {
        Vector3 direction = newPos;
        direction = Vector3.ProjectOnPlane(direction, GetGroundNormal().normalized);
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

    //protected virtual void ApplyMovingFriction(float normalForce, RaycastHit hit)
    //{
    //    float difference = Velocity.magnitude - hit.collider.GetComponent<MovingPlatform3D>().GetVelocity().magnitude;
    //    if (difference < (normalForce * StaticFriction))
    //    {
    //        Velocity = hit.collider.GetComponent<MovingPlatform3D>().GetVelocity();
    //    }
    //    else
    //    {
    //        Velocity += -Velocity.normalized * (normalForce * DynamicFriction);
    //    }
    //}

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

        Vector3 point1 = transform.position + characterCollider.center + Vector3.up * (characterCollider.height / 2 - characterCollider.radius);
        Vector3 point2 = transform.position + characterCollider.center + Vector3.down * (characterCollider.height / 2 - characterCollider.radius);
        if (Physics.CapsuleCast(point1, point2, characterCollider.radius, Vector3.down, groundCheckDistance + skinWidth, CollisionMask))
        {
            return true;
        }
        return false;
    }

    private Vector3 GetGroundNormal()
    {
        Vector3 point1 = transform.position + characterCollider.center + Vector3.up * (characterCollider.height / 2 - characterCollider.radius);
        Vector3 point2 = transform.position + characterCollider.center + Vector3.down * (characterCollider.height / 2 - characterCollider.radius);
        RaycastHit hit;
        if (Physics.CapsuleCast(point1, point2, characterCollider.radius, Vector3.down, out hit, groundCheckDistance + skinWidth, CollisionMask))
        {
            return hit.normal;
        }
        return Vector3.up;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerBaseState")]
public class PlayerBaseState : State
{
    protected CapsuleCollider playerCollider;
    protected int limit = 0;
    protected Camera playerCamera;
    protected float minCameraAngle = -30;
    protected float maxCameraAngle = 75;
    protected Vector3 cameraPosition;
    protected SphereCollider sphere;
    protected Player owner;

    [SerializeField] private LayerMask CollisionMask;
    [SerializeField] private LayerMask CameraCollisionMask;
    private float yaw;
    private float pitch;
    private Vector3 currentRotation;
    private Vector3 rotationSmoothVel;
    private float rotationSmoothTime = 0.1f;
    private float fallMultiplier = 2.5f;
    protected Vector3 Position { get { return owner.transform.position; } set { owner.transform.position = value; } }
    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraPosition = new Vector3(0, owner.yAngle, owner.zAngle);
        sphere = owner.GetComponentInChildren<SphereCollider>();
        playerCollider = owner.GetComponent<CapsuleCollider>();
        playerCamera = owner.GetComponentInChildren<Camera>();
    }

    protected virtual void HandleInput()
    {
        if (Manager.Instance.IsPaused == false)
        {
            owner.RotationX -= Input.GetAxisRaw("Mouse Y") * owner.MouseSensitivity;
            owner.RotationX = Mathf.Clamp(owner.RotationX, minCameraAngle, maxCameraAngle);
            owner.RotationY += Input.GetAxisRaw("Mouse X") * owner.MouseSensitivity;
            playerCamera.transform.rotation = Quaternion.Euler(owner.RotationX, owner.RotationY, 0.0f);
            if (Input.GetKeyDown(KeyCode.V))
                owner.FirstPersonView = !owner.FirstPersonView;
            if (owner.FirstPersonView)
            {
                HandleFirstPersonCamera();
            }
            else
            {
                HandleThirdPersonCamera();
            }
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            if (direction == Vector3.zero)
            {
                owner.DynamicFriction = owner.DefaultDynamicFriction * 3;
                owner.StaticFriction = owner.DefaultStaticFriction * 1.5f;
            }
            else
            {
                owner.DynamicFriction = owner.DefaultDynamicFriction / 1.25f;
                owner.StaticFriction = owner.DefaultStaticFriction;

            }
            direction = Quaternion.LookRotation(Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up)) * direction;
            direction = Vector3.ProjectOnPlane(direction, GetGroundNormal().normalized);
            float distance = owner.Acceleration * Time.deltaTime * owner.SpeedModifier;
            owner.Velocity += direction.normalized * distance;
        }

    }

    protected virtual void ApplyGravity(float gravitMultiplier = 1)
    {
        Vector3 groundNormal = GetGroundNormal();
        if (Vector3.Angle(Vector3.up , groundNormal) > owner.MaxClimbAngle)
        {
            owner.Velocity += Vector3.down * owner.GravityForce * gravitMultiplier * Time.deltaTime;
        }
        else
        {
            owner.Velocity += -groundNormal * owner.GravityForce * gravitMultiplier * Time.deltaTime;
        }
    }

    protected virtual void HandleFirstPersonCamera()
    {
        playerCamera.transform.position = owner.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
    }
    protected virtual void HandleThirdPersonCamera()
    {
        RaycastHit hit;
        Vector3 cameraUpdate = playerCamera.transform.rotation * cameraPosition.normalized;
        if (Physics.SphereCast(owner.transform.position, sphere.radius, cameraUpdate, out hit, cameraPosition.magnitude, CameraCollisionMask))
        {
            Vector3 newPosition = cameraUpdate * (hit.distance - sphere.radius);
            playerCamera.transform.position = newPosition + owner.transform.position;
        }
        else
        {
            playerCamera.transform.position = cameraUpdate * cameraPosition.magnitude + owner.transform.position;
        }
    }

    protected virtual void IsColliding()
    {
        RaycastHit hit;
        Vector3 point1 = owner.transform.position + playerCollider.center + Vector3.up * (playerCollider.height / 2 - playerCollider.radius);
        Vector3 point2 = owner.transform.position + playerCollider.center + Vector3.down * (playerCollider.height / 2 - playerCollider.radius);
        Vector3 projection = Vector3.zero;
        if (Physics.CapsuleCast(point1, point2, playerCollider.radius, owner.Velocity.normalized, out hit, owner.Velocity.magnitude * Time.deltaTime + owner.SkinWidth, CollisionMask) && limit < 25)
        {
            RaycastHit normalHit;
            Physics.CapsuleCast(point1, point2, playerCollider.radius, -hit.normal, out normalHit, owner.Velocity.magnitude * Time.deltaTime + owner.SkinWidth, CollisionMask);
            Vector3 moveDistance = owner.Velocity.normalized * (normalHit.distance - owner.SkinWidth);
            owner.transform.position += moveDistance;
            projection = GetProjection(owner.Velocity, hit.normal);
            owner.Velocity += projection;
            ApplyFriction(projection.magnitude);
            limit++;
            IsColliding();
        }
        limit = 0;
    }

    protected virtual void ApplyFriction(float normalForce)
    {
        if (owner.Velocity.magnitude < (normalForce * owner.StaticFriction ))
        {
            owner.Velocity = Vector3.zero;
        }
        else
        {
            owner.Velocity += -owner.Velocity.normalized * (normalForce * owner.DynamicFriction );
        }
    }

    protected virtual Vector3 GetProjection(Vector3 velocity, Vector3 normal)
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

    protected virtual bool IsGrounded()
    {
        RaycastHit hit;
        Vector3 point1 = owner.transform.position + playerCollider.center + Vector3.up * (playerCollider.height / 2 - playerCollider.radius);
        Vector3 point2 = owner.transform.position + playerCollider.center + Vector3.down * (playerCollider.height / 2 - playerCollider.radius);
        if (Physics.CapsuleCast(point1, point2, playerCollider.radius, Vector3.down, out hit, owner.GroundCheckDistance + owner.SkinWidth, CollisionMask))
        {
            float angle = Vector3.Angle(Vector3.up, hit.normal);
            if (angle > owner.MaxClimbAngle)
            {
                return false;
            }

            return true;
        }
        return false;
    }

    protected virtual Vector3 GetGroundNormal()
    {
        Vector3 point1 = owner.transform.position + playerCollider.center + Vector3.up * (playerCollider.height / 2 - playerCollider.radius);
        Vector3 point2 = owner.transform.position + playerCollider.center + Vector3.down * (playerCollider.height / 2 - playerCollider.radius);
        RaycastHit hit;
        if (Physics.CapsuleCast(point1, point2, playerCollider.radius, Vector3.down, out hit, owner.GroundCheckDistance + owner.SkinWidth, CollisionMask))
        {
            return hit.normal;
        }
        return Vector3.up;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Player)owner;
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{

    public LayerMask CollisionMask;
    [SerializeField] protected float Acceleration = 20.0f;
    [SerializeField] protected float GravityForce = 15.0f;
    [SerializeField] protected float StaticFriction = 0.1f;
    [SerializeField] protected float DynamicFriction = 0.06f;
    [SerializeField] protected float JumpDistance = 5.0f;
    [SerializeField] protected float AirResistance = 0.9f;
    [SerializeField] protected float MouseSensitivity = 3.0f;
    [SerializeField] protected float skinWidth = 0.005f;
    [SerializeField] protected float groundCheckDistance = 0.5f;
    protected CapsuleCollider playerCollider;
    protected int limit = 0;
    protected Camera playerCamera;
    protected float minCameraAngle = -30;
    protected float maxCameraAngle = 40;
    protected Vector3 cameraPosition;
    protected SphereCollider sphere;
    protected Player owner;

    protected Vector3 Position { get { return owner.transform.position; } set { owner.transform.position = value; } }

    public override void Enter()
    {
        cameraPosition = new Vector3(0, 5, -11);
        sphere = owner.GetComponentInChildren<SphereCollider>();
        playerCollider = owner.GetComponent<CapsuleCollider>();
        playerCamera = owner.GetComponentInChildren<Camera>();
    }

    protected virtual void HandleInput()
    {
        owner.RotationX -= Input.GetAxisRaw("Mouse Y") * MouseSensitivity;
        owner.RotationX = Mathf.Clamp(owner.RotationX, minCameraAngle, maxCameraAngle);
        owner.RotationY += Input.GetAxisRaw("Mouse X") * MouseSensitivity;
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
        direction = playerCamera.transform.rotation * direction;
        direction = Vector3.ProjectOnPlane(direction, GetGroundNormal().normalized);
        float distance = Acceleration * Time.deltaTime;
        owner.Velocity += direction.normalized * distance;
    }

    protected virtual void ApplyGravity()
    {
        Vector3 gravity = Vector3.down * GravityForce * Time.deltaTime;
        owner.Velocity += gravity;
    }

    protected virtual void HandleFirstPersonCamera()
    {
        playerCamera.transform.position = owner.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
    }
    protected virtual void HandleThirdPersonCamera()
    {
        RaycastHit hit;
        Vector3 cameraUpdate = playerCamera.transform.rotation * cameraPosition.normalized;
        if (Physics.SphereCast(owner.transform.position, sphere.radius, cameraUpdate, out hit, cameraPosition.magnitude, CollisionMask))
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
        if (Physics.CapsuleCast(point1, point2, playerCollider.radius, owner.Velocity.normalized, out hit, owner.Velocity.magnitude * Time.deltaTime + skinWidth, CollisionMask) && limit < 10)
        {
            RaycastHit normalHit;
            Physics.CapsuleCast(point1, point2, playerCollider.radius, -hit.normal, out normalHit, owner.Velocity.magnitude * Time.deltaTime + skinWidth, CollisionMask);
            Vector3 moveDistance = owner.Velocity.normalized * (normalHit.distance - skinWidth);
            owner.transform.position += moveDistance;
            Vector3 projection = GetProjection(owner.Velocity, hit.normal);
            owner.Velocity += projection;
            ApplyFriction(projection.magnitude);
            limit++;
            IsColliding();
        }
        limit = 0;
    }

    //protected virtual void ApplyMovingFriction(float normalForce, RaycastHit hit)
    //{
    //    float difference = owner.Velocity.magnitude - hit.collider.GetComponent<MovingPlatform3D>().GetVelocity().magnitude;
    //    if (difference < (normalForce * StaticFriction))
    //    {
    //        owner.Velocity = hit.collider.GetComponent<MovingPlatform3D>().GetVelocity();
    //    }
    //    else
    //    {
    //        owner.Velocity += -owner.Velocity.normalized * (normalForce * DynamicFriction);
    //    }
    //}

    protected virtual void ApplyFriction(float normalForce)
    {
        if (owner.Velocity.magnitude < (normalForce * StaticFriction))
        {
            owner.Velocity = Vector3.zero;
        }
        else
        {
            owner.Velocity += -owner.Velocity.normalized * (normalForce * DynamicFriction);
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
        Vector3 point1 = owner.transform.position + playerCollider.center + Vector3.up * (playerCollider.height / 2 - playerCollider.radius);
        Vector3 point2 = owner.transform.position + playerCollider.center + Vector3.down * (playerCollider.height / 2 - playerCollider.radius);
        if (Physics.CapsuleCast(point1, point2, playerCollider.radius, Vector3.down, groundCheckDistance + skinWidth, CollisionMask))
        {
            return true;
        }
        return false;
    }

    protected virtual Vector3 GetGroundNormal()
    {
        Vector3 point1 = owner.transform.position + playerCollider.center + Vector3.up * (playerCollider.height / 2 - playerCollider.radius);
        Vector3 point2 = owner.transform.position + playerCollider.center + Vector3.down * (playerCollider.height / 2 - playerCollider.radius);
        RaycastHit hit;
        if (Physics.CapsuleCast(point1, point2, playerCollider.radius, Vector3.down, out hit, groundCheckDistance + skinWidth, CollisionMask))
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

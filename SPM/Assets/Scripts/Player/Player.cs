using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    #region Properties
    public float Acceleration
    {
        get { return acceleration; }
        set { acceleration = value; }
    }
    public float GravityForce
    {
        get { return gravityForce; }
        set { gravityForce = value; }
    }
    public float TerminalVelocity
    {
        get { return terminalVelocity; }
        set { terminalVelocity = value; }
    }
    public float StaticFriction
    {
        get { return staticFriction; }
        set { staticFriction = value; }
    }
    public float DynamicFriction
    {
        get { return dynamicFriction; }
        set { dynamicFriction = value; }
    }
    public float JumpHeight
    {
        get { return jumpHeight; }
        set { jumpHeight = value; }
    }
    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }
    public float SkinWidth
    {
        get { return skinWidth; }
        set { skinWidth = value; }
    }
    public float GroundCheckDistance
    {
        get { return groundCheckDistance; }
        set { groundCheckDistance = value; }
    }
    public float MaxClimbAngle
    {
        get { return maxClimbAngle; }
        set { maxClimbAngle = value; }
    }
    #endregion

    #region Fields
    [SerializeField] private float acceleration;
    [SerializeField] private float gravityForce;
    [SerializeField] private float staticFriction;
    [SerializeField] private float dynamicFriction;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float terminalVelocity;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float skinWidth;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float maxClimbAngle;
    #endregion

    public LayerMask CollisionMask;
    public Vector3 Velocity;
    [HideInInspector] public float RotationX;
    [HideInInspector] public float RotationY;

    public float yAngle, zAngle;
    //public GameObject Shield;
    public bool FirstPersonView = false;
    public float SpeedModifier;

    protected override void Awake()
    {
        RotationY = 90;
        SpeedModifier = 1f;
        base.Awake();
    }

}

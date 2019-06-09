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
    public float AirResistance
    {
        get { return airResistance; }
        set { airResistance = value; }
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
    public Vector3 BowOffset
    {
        get { return bowOffset; }
        set { bowOffset = value; }
    }
    public float CameraLerpSpeed
    {
        get { return cameraLerpSpeed; }
        set { cameraLerpSpeed = value; }
    }
    #endregion

    #region Fields
    [SerializeField] private float acceleration;
    [SerializeField] private float gravityForce;
    [SerializeField] private float staticFriction;
    [SerializeField] private float dynamicFriction;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float airResistance;
    [SerializeField] private float skinWidth;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float maxClimbAngle;
    [SerializeField] private Vector3 bowOffset;
    [SerializeField] private float cameraLerpSpeed;

    #endregion

    public LayerMask CollisionMask;
    public Vector3 Velocity { get; set; }
    public float TransitionTime { get; set; }
    [HideInInspector] public float RotationX;
    [HideInInspector] public float RotationY;
    public float yAngle, zAngle;
    public bool FirstPersonView = false;
    public float SpeedModifier;
    public float DefaultDynamicFriction { get; set; }
    public float DefaultStaticFriction { get; set; }
    public Animator CharacterAnimator { get; set; }
    protected override void Awake()
    {
        CharacterAnimator = GetComponent<Animator>();
        RotationY = 90;
        RotationX = 25;
        SpeedModifier = 1f;
        TransitionTime = 0.0f;
        DefaultDynamicFriction = DynamicFriction;
        DefaultStaticFriction = StaticFriction;
        base.Awake();
    }

}

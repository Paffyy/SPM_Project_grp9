using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{

    public LayerMask CollisionMask;
    [HideInInspector] public Vector3 Velocity;
    [HideInInspector] public float RotationX;
    [HideInInspector] public float RotationY;
    public GameObject Shield;
    public bool FirstPersonView = false;


    protected override void Awake()
    {
        base.Awake();
    }

}

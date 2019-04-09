using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/ShieldState")]
public class ShieldState : PlayerBaseState
{

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        HandleInput();
        ApplyGravity();
        IsColliding();
        owner.Velocity *= Mathf.Pow(AirResistance, Time.deltaTime);
        Position += owner.Velocity * Time.deltaTime;
        if (IsGrounded())
        {
            owner.Transition<WalkingState>();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            owner.Transition<ShieldState>();
        }

    }

    private void Jump()
    {
        if (IsGrounded())
        {
            Vector3 jump = Vector3.up * JumpDistance;
            owner.Velocity += jump;
        }
    }
}

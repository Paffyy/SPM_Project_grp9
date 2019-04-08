using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/AirState")]
public class AirState : PlayerBaseState
{

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        HandleInput();
        ApplyGravity();
        Jump();
        IsColliding();
        owner.Velocity *= Mathf.Pow(AirResistance, Time.deltaTime);
        Position += owner.Velocity * Time.deltaTime;
        if (IsGrounded())
        {
            owner.Transition<WalkingState>();
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

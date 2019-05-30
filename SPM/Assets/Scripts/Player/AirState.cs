using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/AirState")]
public class AirState : PlayerBaseState
{
    public override void HandleUpdate()
    {
        HandleInput();
        ApplyGravity();
        IsColliding();
        if (owner.Velocity.magnitude > owner.TerminalVelocity)
        {
            owner.Velocity = owner.Velocity.normalized * owner.TerminalVelocity;
        }
        owner.transform.position += owner.Velocity * Time.deltaTime;
        if (IsGrounded())
        {
            owner.Transition<WalkingState>();
        }
    }
}

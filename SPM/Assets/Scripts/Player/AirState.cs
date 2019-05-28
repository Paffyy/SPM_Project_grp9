using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/AirState")]
public class AirState : PlayerBaseState
{
    public override void HandleUpdate()
    {
        HandleInput(true);
        ApplyGravity();
        IsColliding();
        //Position += owner.Velocity * Time.deltaTime;
        if (IsGrounded())
        {
            owner.Transition<WalkingState>();
        }
    }
}

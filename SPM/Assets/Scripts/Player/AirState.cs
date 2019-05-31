using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/AirState")]
public class AirState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        owner.SpeedModifier = 0.8f;
    }
    public override void HandleUpdate()
    {
        HandleInput();
        ApplyGravity();
        IsColliding();
        owner.Velocity *= Mathf.Pow(owner.AirResistance, Time.deltaTime);
        owner.transform.position += owner.Velocity * Time.deltaTime;
        if (IsGrounded())
        {
            owner.Transition<WalkingState>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/WalkingState")]
public class WalkingState : PlayerBaseState
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
        owner.transform.position += owner.Velocity * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
            owner.Transition<AirState>();
    }

}

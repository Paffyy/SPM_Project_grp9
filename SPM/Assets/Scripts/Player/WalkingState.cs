using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/WalkingState")]
public class WalkingState : PlayerBaseState
{

   // private bool shieldActive = false;
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        HandleInput();
        ApplyGravity();
        IsColliding();
        owner.transform.position += owner.Velocity * Time.deltaTime;
        if (Manager.Instance.IsPaused == false && InputManager.Instance.GetkeyDown(KeybindManager.Instance.Jump, InputManager.ControllMode.Play))
        {
            Jump();
        }
    }
    private void Jump()
    {
        if (IsGrounded())
        {
            Vector3 jump = Vector3.up * owner.JumpHeight;
            owner.Velocity += jump;
            owner.Transition<AirState>();
        }
    }
}

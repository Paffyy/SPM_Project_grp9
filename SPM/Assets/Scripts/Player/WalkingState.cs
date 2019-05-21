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
        owner.Velocity *= Mathf.Pow(AirResistance, Time.deltaTime);
        owner.transform.position += owner.Velocity * Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    shieldActive = !shieldActive;
        //}
        //if (shieldActive)
        //{
        //    owner.Shield.SetActive(true);
        //    owner.Shield.GetComponent<Shield>().Reflect();
        //}
        //else
        //{
        //    owner.Shield.SetActive(false);
        //}
        if (isActive == true && Input.GetKeyDown(KeybindManager.Instance.Jump.GetKeyCode()))
            owner.Transition<AirState>();
    }
}

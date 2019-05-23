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

        //TODO fixa det här
        //Vet inte varför man är tvungen att kolla om spelet är pausat här
        //annars hoppar man ändå
        if (Manager.Instance.IsPaused == false && InputManager.Instance.GetkeyDown(KeybindManager.Instance.Jump, InputManager.ControllMode.Play))
        {
            owner.Transition<AirState>();
        }
    }
}

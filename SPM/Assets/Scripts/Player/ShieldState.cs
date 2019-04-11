using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/ShieldState")]
public class ShieldState : WeaponBaseState
{


    public override void Enter()
    {
        //Debug.Log("Shield");
        owner.Shield.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        owner.Shield.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            owner.Transition<NoWeaponState>();
    }

}

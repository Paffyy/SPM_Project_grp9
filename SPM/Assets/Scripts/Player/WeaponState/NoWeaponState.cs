using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/NoWeaponState")]
public class NoWeaponState : WeaponBaseState
{

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            owner.Transition<ShieldState>();
        }

        // lägg till kommandon för att växla till andra vapen
    }
}

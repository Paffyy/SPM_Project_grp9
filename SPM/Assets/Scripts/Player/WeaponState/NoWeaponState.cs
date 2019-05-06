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
            owner.Transition<SwordState>();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            owner.Transition<BowState>();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            owner.Transition<ShieldState>();

        // lägg till kommandon för att växla till andra vapen
    }
}

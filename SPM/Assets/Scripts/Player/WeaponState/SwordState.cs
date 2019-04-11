using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/SwordState")]
public class SwordState : WeaponBaseState
{


    public override void Enter()
    {
        owner.Sword.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        owner.Sword.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            owner.Transition<NoWeaponState>();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            owner.Transition<BowState>();
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            owner.Transition<ShieldState>();

        //kolla ev. om karaktären har tillgång till dessa vapen innan byte av state
    }

}

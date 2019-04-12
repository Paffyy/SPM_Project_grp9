﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/SwordState")]
public class SwordState : WeaponBaseState
{

    public override void Enter()
    {
        owner.SwordIcon.GetComponent<Image>().color = Color.white;
        owner.Sword.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        owner.SwordIcon.GetComponent<Image>().color = Color.clear;
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

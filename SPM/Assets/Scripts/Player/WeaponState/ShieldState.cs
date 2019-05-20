﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/ShieldState")]
public class ShieldState : WeaponBaseState
{
    public override void Enter()
    {
        owner.player.SpeedModifier = 0.7f;
        owner.ShieldIcon.GetComponent<Image>().color = Color.green;
        if (owner.Shield != null)
        {
            owner.Shield.GetComponent<Shield>().UpdateTransformation();
            owner.Shield.SetActive(true);
        }
        base.Enter();
    }

    public override void Exit()
    {
        owner.player.SpeedModifier = 1f;
        owner.ShieldIcon.GetComponent<Image>().color = Color.white;
        if (owner.Shield != null)
        {
            owner.Shield.SetActive(false);
        } 
    }

    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            owner.Transition<NoWeaponState>();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            owner.Transition<BowState>();
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            owner.Transition<SwordState>();
    }
}

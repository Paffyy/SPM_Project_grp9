using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Weapon/BowState")]
public class BowState : WeaponBaseState
{
    public override void Enter()
    {
        owner.BowIcon.GetComponent<Image>().color = Color.green;
        owner.Bow.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        owner.BowIcon.GetComponent<Image>().color = Color.white;
        owner.Bow.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            owner.Transition<NoWeaponState>();
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            owner.Transition<SwordState>();
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //    owner.Transition<ShieldState>();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/ShieldState")]
public class ShieldState : WeaponBaseState
{


    public override void Enter()
    {
        //Debug.Log("Shield");
        if (owner.Shield != null)
        {
            owner.Shield.SetActive(true);
        }
        base.Enter();
    }

    public override void Exit()
    {
        if(owner.Shield != null)
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
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            owner.Transition<SwordState>();
    }

}

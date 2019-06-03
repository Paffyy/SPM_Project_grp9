using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Weapon/BowState")]
public class BowState : WeaponBaseState
{
    public override void Enter()
    {
        owner.CurrentStateID = 1;
        //owner.BowIcon.GetComponent<Image>().color = Color.green;
        //owner.WeponsPanel.SelectWeapon(2);
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.EquipmentSlot1, InputManager.ControllMode.Play))
        {
            
            owner.Transition<SwordState>();
        }
        else if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.BlockAndAim, InputManager.ControllMode.Play))
        {
            owner.Transition<BowAimState>();
        }
    }

    public override void Exit()
    {
        //owner.WeponsPanel.SelectWeapon(2);
        base.Exit();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/BowAimState")]
public class BowAimState : WeaponBaseState
{
    public override void Enter()
    {
        owner.CurrentStateID = 1;
        owner.WeponsPanel.SelectWeapon(2, true);
        owner.BowFirstPerson.SetActive(true);
    }

    public override void Exit()
    {
        owner.WeponsPanel.SelectWeapon(2,false);
        owner.BowFirstPerson.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.EquipmentSlot1, InputManager.ControllMode.Play) && owner.player.TransitionTime <= 0)
        {
            owner.Transition<SwordState>();
            owner.player.TransitionTime = 0.1f;
        }
        //if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.BlockAndAim, InputManager.ControllMode.Play))
        //{
        //    owner.Transition<BowState>();
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //    owner.Transition<ShieldState>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/SwordState")]
public class SwordState : WeaponBaseState
{
    public override void Enter()
    {
        owner.CurrentStateID = 0;
        owner.WeponsPanel.SelectWeapon(1, true);
        owner.Sword.SetActive(true);
        owner.Shield.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        owner.WeponsPanel.SelectWeapon(1, false);
        owner.Shield.GetComponent<Shield>().IsBlocking = false;
        owner.Sword.SetActive(false);
        owner.Shield.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (!owner.Sword.GetComponent<Sword>().IsBladeStorming)
        {
            if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.EquipmentSlot2, InputManager.ControllMode.Play) && owner.player.TransitionTime <= 0)
            {
                owner.Transition<BowAimState>();
                owner.player.TransitionTime = 0.1f;
            }
        }
    }

}

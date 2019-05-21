using System.Collections;
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
        if(Manager.Instance.IsPaused == false)
        {
            if (Input.GetKeyDown(KeybindManager.Instance.EquipmentSlot2.GetKeyCode()))
                owner.Transition<BowState>();
            else if (Input.GetKeyDown(KeybindManager.Instance.EquipmentSlot1.GetKeyCode()))
                owner.Transition<SwordState>();
        }
    }
}

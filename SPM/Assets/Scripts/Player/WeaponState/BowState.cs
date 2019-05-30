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
        owner.BowIcon.GetComponent<Image>().color = Color.green;
        owner.Bow.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        owner.Bow.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.EquipmentSlot1, InputManager.ControllMode.Play))
        {
            owner.BowIcon.GetComponent<Image>().color = Color.white;
            owner.Transition<SwordState>();
        }
        else if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.BlockAndAim, InputManager.ControllMode.Play))
        {
            owner.Transition<BowAimState>();
        }
    }

}
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
        owner.BowIcon.GetComponent<Image>().color = Color.green;
        owner.Bow.SetActive(true);
        owner.BowFirstPerson.SetActive(true);
    }

    public override void Exit()
    {
        owner.BowFirstPerson.SetActive(false);
        owner.Bow.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.EquipmentSlot1, InputManager.ControllMode.Play))
        {
            owner.BowIcon.GetComponent<Image>().color = Color.white;
            owner.Transition<SwordState>();
        }
        //if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.BlockAndAim, InputManager.ControllMode.Play))
        //{
        //    owner.Transition<BowState>();
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //    owner.Transition<ShieldState>();
    }

}

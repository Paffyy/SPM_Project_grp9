using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/BowAimState")]
public class BowAimState : WeaponBaseState
{
    public override void Enter()
    {
        owner.BowFirstPerson.SetActive(true);
    }

    public override void Exit()
    {
        owner.BowFirstPerson.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeybindManager.Instance.EquipmentSlot1.GetKeyCode()))
        {
            owner.BowIcon.GetComponent<Image>().color = Color.white;
            owner.Transition<SwordState>();
        }
        if (Input.GetKeyDown(KeybindManager.Instance.BlockAndAim.GetKeyCode()))
        {
            owner.Transition<BowState>();
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //    owner.Transition<ShieldState>();
    }

}

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
        //owner.BowIcon.GetComponent<Image>().color = Color.white;
        owner.Bow.SetActive(false);
    }

    public override void HandleUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //    owner.Transition<NoWeaponState>();
        if (Manager.Instance.IsPaused == false &&  Input.GetKeyDown(KeybindManager.Instance.EquipmentSlot1.GetKeyCode()))
        {
            owner.BowIcon.GetComponent<Image>().color = Color.white;
            //owner.Bow.SetActive(false);
            owner.Transition<SwordState>();
        }
        else if (Manager.Instance.IsPaused == false &&  Input.GetKeyDown(KeybindManager.Instance.BlockAndAim.GetKeyCode()))
        {
            //owner.Bow.SetActive(false);
            owner.Transition<BowAimState>();
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //    owner.Transition<ShieldState>();
    }

}
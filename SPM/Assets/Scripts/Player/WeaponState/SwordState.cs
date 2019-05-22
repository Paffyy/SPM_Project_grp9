using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/SwordState")]
public class SwordState : WeaponBaseState
{

    public override void Enter()
    {
        owner.SwordIcon.GetComponent<Image>().color = Color.green;
        owner.Sword.SetActive(true);
        owner.Shield.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        owner.SwordIcon.GetComponent<Image>().color = Color.white;
        owner.Shield.GetComponent<Shield>().IsBlocking = false;
        owner.Sword.SetActive(false);
        owner.Shield.SetActive(false);
    }

    public override void HandleUpdate()
    {
        if (!owner.Sword.GetComponent<Sword>().IsBladeStorming)
        {
            //if (Input.GetKeyDown(KeyCode.Alpha0))
            //    owner.Transition<NoWeaponState>();
            if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.EquipmentSlot2, InputManager.ControllMode.Play))
                owner.Transition<BowState>();
            //else if (Input.GetKeyDown(KeyCode.Alpha3))
            //    owner.Transition<ShieldState>();
        }

        //kolla ev. om karaktären har tillgång till dessa vapen innan byte av state
    }

}

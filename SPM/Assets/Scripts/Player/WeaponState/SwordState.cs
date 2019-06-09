using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/SwordState")]
public class SwordState : WeaponBaseState
{
    [SerializeField]
    private AudioClip clip;

    public override void Enter()
    {
        AudioEventInfo audioEvent = new AudioEventInfo(clip);
        EventHandler.Instance.FireEvent(EventHandler.EventType.AudioEvent, audioEvent);

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
                owner.player.CharacterAnimator.SetBool("Sword", false);
                owner.player.CharacterAnimator.SetBool("Bow", true);
                owner.Transition<BowAimState>();
                owner.player.TransitionTime = 0.1f;
            }
        }
    }

}

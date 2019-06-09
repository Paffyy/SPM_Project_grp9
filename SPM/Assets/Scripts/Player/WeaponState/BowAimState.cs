using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/BowAimState")]
public class BowAimState : WeaponBaseState
{
    [SerializeField]
    private AudioClip clip;

    public override void Enter()
    {
        AudioEventInfo audioEvent = new AudioEventInfo(clip);
        EventHandler.Instance.FireEvent(EventHandler.EventType.AudioEvent, audioEvent);

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
            owner.player.CharacterAnimator.SetBool("Bow", false);
            owner.player.CharacterAnimator.SetBool("Sword", true);
            owner.Transition<SwordState>();
            owner.player.TransitionTime = 0.1f;
        }
    }
}

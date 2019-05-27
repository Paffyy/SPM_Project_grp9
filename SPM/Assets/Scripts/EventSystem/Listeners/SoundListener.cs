using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, PlayDeathSound);
        EventHandler.Instance.Register(EventHandler.EventType.PickUpEvent, PlayPickUpSound);
    }

    void Start()
    {
        Register();
    }

    private void PlayDeathSound(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if (deathEventInfo != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void PlayPickUpSound(BaseEventInfo e)
    {
        var pickUpEventInfo = e as PickupEventInfo;
        if(pickUpEventInfo != null)
        {
            audioSource.clip = pickUpEventInfo.PickUpObject.GetComponent<PickUp>().PickUpSound;
            audioSource.Play();
        }
    
    }
}


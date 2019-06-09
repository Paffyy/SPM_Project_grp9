using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    [SerializeField]
    private AudioSource primaryAudioSource;
    [SerializeField]
    private AudioSource secondaryAudioSource;
    [SerializeField]
    private AudioClip revSound;

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.PickUpEvent, PlayPickUpSound);
        EventHandler.Instance.Register(EventHandler.EventType.AudioEvent, PlayAudioClip);
        EventHandler.Instance.Register(EventHandler.EventType.RevAudioEvent, PlayRevZoneClip);
    }

    void Start()
    {
        Register();
    }

    private void PlayPickUpSound(BaseEventInfo e)
    {
        var pickUpEventInfo = e as PickupEventInfo;
        if(pickUpEventInfo != null)
        {
            if (!primaryAudioSource.isPlaying)
            {
                primaryAudioSource.volume = 0.5f;
                primaryAudioSource.clip = pickUpEventInfo.PickUpObject.GetComponent<PickUp>().PickUpSound;
                primaryAudioSource.Play();
            }

        }

    }

    private void PlayAudioClip(BaseEventInfo e)
    {
        var audio = e as AudioEventInfo;
        if (audio != null)
        {
            if (!primaryAudioSource.isPlaying)
            {
                primaryAudioSource.volume = 0.1f;
                primaryAudioSource.clip = audio.AudioClip;
                primaryAudioSource.Play();
            }
        }
    }

    private void PlayRevZoneClip(BaseEventInfo e)
    {
        secondaryAudioSource.clip = revSound;
        secondaryAudioSource.Play();
    }

}


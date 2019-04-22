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
                audioSource.clip = deathEventInfo.GameObject.GetComponent<DestroyableGameObject>().DeathSound;
                audioSource.Play();
            }
        }
    }
}

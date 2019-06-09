using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundListener : MonoBehaviour
{
    private AudioSource audioSource;
    private float soundDelayTime = 0;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.EnemyAttackEvent, PlayAudioClip);
    }
    void Start()
    {
        Register();
    }
    private void Update()
    {
        soundDelayTime -= Time.deltaTime;
    }
    private void PlayAudioClip(BaseEventInfo e)
    {
        var audio = e as AudioEventInfo;
        if (audio != null)
        {
            if (soundDelayTime < 0 )
            {
                audio.AudioSource.clip = audio.AudioClip;
                soundDelayTime = 0.25f;
                audio.AudioSource.Play();
            }
        }
    }
}

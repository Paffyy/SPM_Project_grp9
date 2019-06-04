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
                audio.audioSource.clip = audio.audioClip;
                soundDelayTime = 0.1f;
                audio.audioSource.Play();
            }
        }
    }
}

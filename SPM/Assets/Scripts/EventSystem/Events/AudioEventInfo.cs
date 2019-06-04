using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventInfo : BaseEventInfo
{

    public AudioClip audioClip;
    public AudioSource audioSource;
    public AudioEventInfo(AudioClip audio)
    {
        audioClip = audio;
    }
    public AudioEventInfo(AudioClip audio, AudioSource source)
    {
        audioClip = audio;
        audioSource = source;
    }
}

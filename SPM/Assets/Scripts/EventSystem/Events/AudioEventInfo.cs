using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventInfo : BaseEventInfo
{
    public AudioClip AudioClip { get; set; }
    public AudioSource AudioSource { get; set; }

    public AudioEventInfo(AudioClip audio)
    {
        AudioClip = audio;
    }
    public AudioEventInfo(AudioClip audio, AudioSource source)
    {
        AudioClip = audio;
        AudioSource = source;
    }
}

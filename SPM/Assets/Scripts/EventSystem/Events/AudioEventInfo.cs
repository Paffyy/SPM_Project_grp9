using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventInfo : BaseEventInfo
{

    public AudioClip audioClip;

    public AudioEventInfo(AudioClip audio)
    {
        audioClip = audio;
    }
}

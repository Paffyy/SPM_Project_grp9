using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventInfo : BaseEventInfo
{
    public AnimatorStateInfo AnimStateInfo { get; set; }

    public AnimEventInfo(AnimatorStateInfo state)
    {
        AnimStateInfo = state;
    }
}

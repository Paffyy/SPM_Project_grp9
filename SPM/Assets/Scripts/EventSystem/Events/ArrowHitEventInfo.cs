using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitEventInfo : BaseEventInfo
{
    public GameObject Arrow;
    public GameObject TargetHit;
    public ArrowHitEventInfo(GameObject arrow)
    {
        Arrow = arrow;
    }
}

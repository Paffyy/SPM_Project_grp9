using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEventInfo : BaseEventInfo
{
    public GameObject CheckPoint { get; set; }

    public CheckPointEventInfo(GameObject newCheckPoint)
    {
        CheckPoint = newCheckPoint;
    }
}

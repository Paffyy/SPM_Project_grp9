using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEventInfo : BaseEventInfo
{
    public GameObject CheckPoint;

    public CheckPointEventInfo(GameObject newCheckPoint)
    {
        CheckPoint = newCheckPoint;
    }
}

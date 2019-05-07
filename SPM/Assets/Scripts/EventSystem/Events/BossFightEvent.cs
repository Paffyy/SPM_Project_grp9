using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightEventInfo : BaseEventInfo
{
    public bool FightOn;
    public BossFightEventInfo(bool fight)
    {
        FightOn = fight;
    }
}

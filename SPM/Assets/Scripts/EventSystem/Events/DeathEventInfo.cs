using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventInfo : BaseEventInfo
{
    public GameObject GameObject { get; set; }

    public DeathEventInfo( GameObject gameObject)
    {
        GameObject = gameObject;
    }
}

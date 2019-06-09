using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEventInfo : BaseEventInfo
{
    public GameObject PickUpObject { get; set; }

    public PickupEventInfo(GameObject PickUp)
    {
        PickUpObject = PickUp;
    }
}

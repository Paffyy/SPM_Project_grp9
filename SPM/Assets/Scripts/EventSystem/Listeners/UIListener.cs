using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIListener : MonoBehaviour
{
    void Start()
    {
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.PickUpEvent, UpdateUI);
    }

    private void UpdateUI(BaseEventInfo e)
    {
        var pickUpEventInfo = e as PickupEventInfo;
        if(pickUpEventInfo != null)
        {

        }
    }
}

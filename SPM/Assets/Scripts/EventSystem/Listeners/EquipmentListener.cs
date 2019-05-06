using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentListener : MonoBehaviour
{
    public GameObject PlayerObject;

    void Start()
    {
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.PickUpEvent, UpdateEquipment);
    }

    private void UpdateEquipment(BaseEventInfo e)
    {
        var pickUpEventInfo = e as PickupEventInfo;
        if (pickUpEventInfo != null)
        {
            if (pickUpEventInfo.PickUpObject.CompareTag("Arrows"))
            {
                PlayerObject.GetComponent<Weapon>().Bow.GetComponent<Bow>().AddArrows(pickUpEventInfo.PickUpObject.GetComponent<Arrows>().ArrowAmount);
            } else if (pickUpEventInfo.PickUpObject.CompareTag("HealthPotion"))
            {
                PlayerObject.GetComponent<PlayerHealth>().AddHealth(pickUpEventInfo.PickUpObject.GetComponent<HealthPotion>().HealthAmount);
            }
            
        }
    }
}

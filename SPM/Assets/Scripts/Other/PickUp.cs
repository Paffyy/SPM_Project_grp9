using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip PickUpSound;
    public string UIAlertText;
    public float PickUpID;

    void Awake()
    {
        PickUpID = transform.position.sqrMagnitude;
        GameController.GameControllerInstance.PickUps.Add(PickUpID, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupEventInfo pickUpEventInfo = new PickupEventInfo(gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.PickUpEvent, pickUpEventInfo);
            if (!GameController.GameControllerInstance.PickedUpObjects.Contains(PickUpID))
            {
                GameController.GameControllerInstance.PickedUpObjects.Add(PickUpID);
            }
            gameObject.SetActive(false);
        }
    }
}

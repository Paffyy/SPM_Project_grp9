using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip PickUpSound;
    public string UIAlertText;
    public float PickUpID;
    // Start is called before the first frame update
    void Start()
    {
        PickUpID = transform.position.sqrMagnitude;
        GameControl.GameController.PickUps.Add(PickUpID, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupEventInfo pickUpEventInfo = new PickupEventInfo(gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.PickUpEvent, pickUpEventInfo);
            GameControl.GameController.PickedUpObjects.Add(PickUpID);
            gameObject.SetActive(false);
        }
    }
}

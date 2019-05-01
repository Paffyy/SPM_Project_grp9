using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip PickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        
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
            gameObject.SetActive(false);
        }
    }
}

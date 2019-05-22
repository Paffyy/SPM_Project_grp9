using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform RespawnPosition;
    private BoxCollider triggerCollider;

    void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //SaveSystem.SaveGame();
            //SaveEventInfo saveEventInfo = new SaveEventInfo("Reached new checkpoint! auto saving...");
            //EventHandler.Instance.FireEvent(EventHandler.EventType.SaveEvent, saveEventInfo);
            CheckPointEventInfo checkPointEventInfo = new CheckPointEventInfo(gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.CheckPointEvent, checkPointEventInfo);
            triggerCollider.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform RespawnPosition;
    void Start()
    {
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SaveSystem.SaveGame();
            SaveEventInfo saveEventInfo = new SaveEventInfo("Reached new checkpoint! auto saving...");
            EventHandler.Instance.FireEvent(EventHandler.EventType.SaveEvent, saveEventInfo);
            CheckPointEventInfo checkPointEventInfo = new CheckPointEventInfo(gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.CheckPointEvent, checkPointEventInfo);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}

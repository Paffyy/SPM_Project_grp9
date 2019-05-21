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
            CheckPointEventInfo checkPointEventInfo = new CheckPointEventInfo(gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.CheckPointEvent, checkPointEventInfo);
        }
    }
}

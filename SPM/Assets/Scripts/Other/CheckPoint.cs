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
            CheckPointEventInfo checkPointEventInfo = new CheckPointEventInfo(gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.CheckPointEvent, checkPointEventInfo);
            triggerCollider.enabled = false;
        }
    }
}

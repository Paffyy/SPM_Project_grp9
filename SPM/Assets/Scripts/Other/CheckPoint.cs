using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // Manager.Instance.SetCheckPoint(transform.position); // temp
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckPointEventInfo checkPointEventInfo = new CheckPointEventInfo(gameObject);
            EventHandler.Instance.FireEvent(EventHandler.EventType.CheckPointEvent, checkPointEventInfo);
        }
    }
}

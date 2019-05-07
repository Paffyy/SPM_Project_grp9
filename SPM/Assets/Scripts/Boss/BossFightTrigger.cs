using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventHandler.Instance.FireEvent(EventHandler.EventType.BossFightTrigger, new BossFightEventInfo(true));
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    private bool hasBeedTrigerd = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && hasBeedTrigerd == false)
        {
            EventHandler.Instance.FireEvent(EventHandler.EventType.BossFightTrigger, new BossFightEventInfo(true));
            hasBeedTrigerd = true;
        }
    }

}

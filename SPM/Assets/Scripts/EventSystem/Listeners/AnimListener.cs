using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimListener : MonoBehaviour
{
    public HandsAttack rightHand;
    public HandsAttack leftHand;

    public void Start()
    {
        Register();
    }
    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.AnimTriggerEvent, RunAnim);
    }

    public void RunAnim(BaseEventInfo e)
    {
        var animEvent = e as AnimEventInfo;
        if(animEvent != null)
        {
            
        }
    }
}

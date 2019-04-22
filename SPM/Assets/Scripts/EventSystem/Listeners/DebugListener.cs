using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugListener : MonoBehaviour
{
    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, DebugInfo);
    }

    void Start()
    {
        Register();
    }

    void DebugInfo(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if (deathEventInfo != null)
        {
            Debug.Log(deathEventInfo.GameObject.name);
        }
        var debugEventInfo = e as DebugEventInfo;
        if (debugEventInfo != null)
        {
            Debug.Log(debugEventInfo.DebugInfoText);
        }

    }
}

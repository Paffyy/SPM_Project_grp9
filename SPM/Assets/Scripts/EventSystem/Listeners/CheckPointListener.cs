using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointListener : MonoBehaviour
{
    public GameObject FirstCheckPoint;
    private GameObject CurrentCheckPoint;

    void Start()
    {
        CurrentCheckPoint = FirstCheckPoint;
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.CheckPointEvent, UpdateCheckPoint);
        EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, RespawnPlayer);
    }

    private void UpdateCheckPoint(BaseEventInfo e)
    {
        var checkPointEventInfo = e as CheckPointEventInfo;
        if (checkPointEventInfo != null)
        {
            CurrentCheckPoint = checkPointEventInfo.CheckPoint;
        }
    }

    private void RespawnPlayer(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if(deathEventInfo != null)
        {
            if (deathEventInfo.GameObject.CompareTag("Player"))
            {
                deathEventInfo.GameObject.transform.position = CurrentCheckPoint.transform.position;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathListener : MonoBehaviour
{
    public float ExplodeTime;
    public float ScaleFactor;
    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, DestroyObject);
        EventHandler.Instance.Register(EventHandler.EventType.IsDyingEvent, IsDying);
    }
    // Start is called before the first frame update
    void Start()
    {
        Register();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            EventHandler.Instance.Unregister(EventHandler.EventType.IsDyingEvent, IsDying);
            EventHandler.Instance.Unregister(EventHandler.EventType.DeathEvent, DestroyObject);
        }
    }
    void DestroyObject(BaseEventInfo e)
    {
        StartCoroutine(DelayedDeath(0.5f, e));
    }
    void IsDying(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if (deathEventInfo != null)
        {
            StartCoroutine(Grow(deathEventInfo.GameObject.GetComponent<DestroyableGameObject>(), deathEventInfo));
        }
    }
    IEnumerator DelayedDeath(float seconds, BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        deathEventInfo.GameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(seconds);
        if (deathEventInfo != null)
        {
            Destroy(deathEventInfo.GameObject);
        }
    }
    IEnumerator Grow(DestroyableGameObject destroyableGameObject, DeathEventInfo deathEventInfo)
    {
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(ExplodeTime / 25f);
            destroyableGameObject.transform.localScale *= 1 + ScaleFactor;
        }
        EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, new DeathEventInfo(deathEventInfo.GameObject));
    }
}

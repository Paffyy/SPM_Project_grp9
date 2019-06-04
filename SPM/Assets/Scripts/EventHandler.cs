using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void EventListener(BaseEventInfo e);
    public enum EventType { DeathEvent, SpawnEvent, ArrowAoeHitEvent, DamageEvent, IsDyingEvent, RevitalizeEvent, RevitalizeZoneEvent, PickUpEvent, CheckPointEvent, CoolDownEvent, AnimTriggerEvent, BossFightTrigger, SaveEvent, LoadEvent, UIButtonEvent, WeapondHitEvent, ParticleEvent, AudioEvent,
        ShieldBlock
    }
    private Dictionary<EventType, List<EventListener>> eventListeners;
    private static EventHandler instance;
    public static EventHandler Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<EventHandler>();
            return instance;
        }
    }

    public void Register(EventType type, EventListener e)
    {
        if (!eventListeners.ContainsKey(type))
        {
            eventListeners.Add(type, new List<EventListener> { e });
        }
        else
        {
            var listeners = eventListeners[type];
            if (listeners == null)
                listeners = new List<EventListener>();
            listeners.Add(e);
        }
    }
    public void Unregister(EventType type, EventListener e)
    {
        if (!eventListeners.ContainsKey(type))
            return;
        var listeners = eventListeners[type];
        if (listeners == null)
            return;
        else if (listeners.Contains(e))
            listeners.Remove(e);
    }
    public void FireEvent(EventType type, BaseEventInfo e)
    {
        if (eventListeners == null || !eventListeners.ContainsKey(type) || eventListeners[type] == null)
            return;
        foreach (var item in eventListeners[type])
        {
            item(e);
        }
    }

    void Awake()
    {
        eventListeners = new Dictionary<EventType, List<EventListener>>();
    }
}

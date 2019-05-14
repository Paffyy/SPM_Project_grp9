using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeZone : MonoBehaviour
{
    public List<GameObject> Objectives;
    public float TransitionDelay;
    private float timer;
    private bool shouldRevitalize;
    private bool hasRevitalized;
    public void Register()
    {
        if (EventHandler.Instance != null)
        {
            EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, CheckIfShouldRevitalize);
        }
    }

    private void Start()
    {
        Register();
        if (Objectives.Count == 0 )
        {
            hasRevitalized = true; // if objectives count == 0, don't revitalize immediately;
        }
        timer = 1;
    }

    private void Update()
    {
        if (!hasRevitalized)
        {
            if (shouldRevitalize)
            {
                RevitalizeTheZone();
                hasRevitalized = true;
            }
            else if (Objectives != null && Objectives.Count == 0)
            {
                shouldRevitalize = true;
            }
        }
    }
    private void CheckIfShouldRevitalize(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if (deathEventInfo != null)
        {
            if (Objectives.Contains(deathEventInfo.GameObject))
            {
                Objectives.Remove(deathEventInfo.GameObject);
            }
        }
    }

    private void RevitalizeTheZone()
    {
        foreach (Transform child in transform)
        {
            var rev = child.GetComponent<RevitalizeGeometry>();
            if (rev != null && !rev.IsRevitalized)
            {
                rev.Revitalize(TransitionDelay);
            }
            else
            {
                foreach (Transform grandchildren in child)
                {
                    var rev2 = grandchildren.GetComponent<RevitalizeGeometry>();
                    if (rev2 != null && !rev.IsRevitalized)
                    {
                        rev2.Revitalize(TransitionDelay);
                    }
                }
            }
        }
    }
}

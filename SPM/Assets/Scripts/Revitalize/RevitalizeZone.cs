using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeZone : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> revitalizeObjectives;

    private bool shouldRevitalize;
    private bool hasRevitalized;
    private float transitionDelay = 0.5f;
    private float zoneID;

    private List<RevitalizeGeometry> revitalizeObjects;

    public void Register()
    {
        if (EventHandler.Instance != null)
        {
            EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, CheckIfShouldRevitalize);
        }
    }

    void Awake()
    {
        zoneID = transform.position.sqrMagnitude;
        GameController.GameControllerInstance.Zones.Add(zoneID, gameObject);
        revitalizeObjects = GetRevitalizeObjects();
    }

    private void Start()
    {
        Register();
        if (revitalizeObjectives.Count == 0 )
        {
            hasRevitalized = true; // if objectives count == 0, don't revitalize immediately;
        }
    }

    private List<RevitalizeGeometry> GetRevitalizeObjects()
    {
        List<RevitalizeGeometry> revitalizeObjects = new List<RevitalizeGeometry>();
        foreach (Transform child in transform)
        {
            RevitalizeGeometry rev = child.GetComponent<RevitalizeGeometry>();
            if (rev != null)
            {
                revitalizeObjects.Add(rev);
            }
            foreach (Transform grandchildren in child)
            {
                var rev2 = grandchildren.GetComponent<RevitalizeGeometry>();
                if (rev2 != null)
                {
                    revitalizeObjects.Add(rev2);
                }
            }
        }
        return revitalizeObjects;
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
            else if (revitalizeObjectives != null && revitalizeObjectives.Count == 0)
            {
                shouldRevitalize = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RevitalizeTheZone();
            hasRevitalized = true;
        }
    }
    private void CheckIfShouldRevitalize(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if (deathEventInfo != null)
        {
            if (revitalizeObjectives.Contains(deathEventInfo.GameObject))
            {
                revitalizeObjectives.Remove(deathEventInfo.GameObject);
            }
        }
    }

    private void RevitalizeTheZone()
    {
        GameController.GameControllerInstance.RevitalizedZones.Add(zoneID);
        foreach (var item in revitalizeObjects)
        {
            if (!item.IsRevitalized)
            {
                item.Revitalize(transitionDelay);
            }
        }
    }

    public void RevitalizeTheZoneInstant()
    {
        hasRevitalized = true;
        foreach (var item in revitalizeObjects)
        {
            if (!item.IsRevitalized)
            {
                item.InstantRevitalize();
            }
        }
    }
}

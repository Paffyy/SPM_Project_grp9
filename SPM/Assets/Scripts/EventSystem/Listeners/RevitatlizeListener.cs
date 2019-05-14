using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitatlizeListener : MonoBehaviour
{
    public float RevitalizeRadius;
    public LayerMask RevitalizeMask;
    public int distanceModifier;
    private float revitalizeCooldown;
    public void Register()
    {
        if (EventHandler.Instance != null)
        {
            EventHandler.Instance.Register(EventHandler.EventType.RevitalizeEvent, RevitalizeObjects);
        }
    }

    void Start()
    {
        Register();
    }
    void Update()
    {
        if (revitalizeCooldown > 0)
        {
            revitalizeCooldown -= Time.deltaTime;
        }
    }
    void RevitalizeObjects(BaseEventInfo e)
    {
        if (revitalizeCooldown <= 0) // make particle effect mby
        {
            var arrowHitEventInfo = e as ArrowHitEventInfo;
            if (arrowHitEventInfo != null)
            {
                var pos = arrowHitEventInfo.Arrow.transform.position;
                if (arrowHitEventInfo.Arrow.GetComponent<Arrow>().isActiveAndEnabled)
                {
                    var closeRevObjects = Manager.Instance.GetAoeHit(pos, RevitalizeMask, RevitalizeRadius);
                    if (closeRevObjects != null)
                    {
                        revitalizeCooldown = 1;
                        foreach (var item in closeRevObjects)
                        {
                            var revScript = item.GetComponent<RevitalizeGeometry>();
                            var distanceMod = Vector3.Distance(pos, item.transform.position) / distanceModifier;
                            if (revScript.IsRevitalized)
                            {
                                //revScript.DullMaterial(distanceMod);
                            }
                            else
                            {
                                revScript.Revitalize(distanceMod);
                            }
                        }
                    }
                }
            }
        }
    }

}

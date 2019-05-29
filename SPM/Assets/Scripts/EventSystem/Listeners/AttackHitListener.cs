using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitListener : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticelsEffect;


    private void Start()
    {
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.WeapondHitEvent, HandleHit);
    }

    private void HandleHit(BaseEventInfo e)
    {
        var hitEvent = e as AttackHitEventInfo;
        if (hitEvent != null)
        {
            Vector3 particlePos =  hitEvent.TargetHit.ClosestPointOnBounds(hitEvent.self.transform.position);
            ParticleSystem part = Instantiate(hitParticelsEffect, particlePos, Quaternion.identity, transform);
            part.Play();
            
        }
    }

}

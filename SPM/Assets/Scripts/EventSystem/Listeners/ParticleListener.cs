using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleListener : MonoBehaviour
{
    [SerializeField] private LayerMask aOECollidersToHit;
    [SerializeField] private ParticleSystem revParticleEffect;
    [SerializeField] private ParticleSystem arrowSpecialParticleEffect;
    [SerializeField] private ParticleSystem hitParticelsEffect;
    [SerializeField] private ParticleSystem hitHeartParticelsEffect;
    private Vector3 aOEverticalParticlesOffset = new Vector3(0, 0.2f, 0);
    private Vector3 aOEyOffset = new Vector3(0, 1, 0);


    private void Start()
    {
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.ArrowAoeHitEvent, DoAoeArrowAttack);
        EventHandler.Instance.Register(EventHandler.EventType.WeapondHitEvent, HandleHit);
    }

    private void DoAoeArrowAttack(BaseEventInfo e)
    {
        var arrowEventInfo = e as ArrowHitEventInfo;
        if (arrowEventInfo != null)
        {
            Transform arrowHitLocation = arrowEventInfo.Arrow.transform;
            Arrow arrowScript = arrowEventInfo.Arrow.GetComponent<Arrow>();
            var enemiesInArea = Manager.Instance.GetAoeHit(arrowHitLocation.position, aOECollidersToHit, arrowScript.AoeRadius);
            if (!arrowSpecialParticleEffect.isPlaying)
            {
                transform.position = arrowHitLocation.position + aOEverticalParticlesOffset;
                arrowSpecialParticleEffect.Play();
            }
         
            foreach (var item in enemiesInArea)
            {
                var enemyHealth = item.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(arrowScript.AoeDamage, true);
                    var revEffect = Instantiate(revParticleEffect, item.transform.position + aOEyOffset, item.transform.rotation);
                    revEffect.Play();
                    PlayHeartParticles(item);
                    //Debug.Log(revEffect.transform.position);
                    //Destroy(revEffect, 2.5f);
                }
            }
        }
    }

    private void HandleHit(BaseEventInfo e)
    {
        var hitEvent = e as AttackHitEventInfo;
        if (hitEvent != null)
        {
            Vector3 particlePos;
            if (hitEvent.WeapondsUsed == AttackHitEventInfo.Weapon.Sword)
            {
                particlePos = hitEvent.TargetHit.ClosestPointOnBounds(hitEvent.self.transform.position);
                ParticleSystem part = Instantiate(hitParticelsEffect, particlePos, Quaternion.identity, transform);
                part.Play();
            }
            else
            {
                
                PlayHeartParticles(hitEvent.TargetHit);
            }

        }
    }

    private void PlayHeartParticles(Collider hit)
    {
        //kanske lite för hackigt, den hämtar den högsta postionen på collidern
        Vector3 particlePos = hit.ClosestPointOnBounds(Vector3.up * 100);
        ParticleSystem partHeart = Instantiate(hitHeartParticelsEffect, particlePos, Quaternion.identity, transform);
        partHeart.Play();
    }
}

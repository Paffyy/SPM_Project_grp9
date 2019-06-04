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
    private Vector3 aOEyOffset = new Vector3(0, 0.5f, 0);


    private void Start()
    {
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.ArrowAoeHitEvent, DoAoeArrowAttack);
        EventHandler.Instance.Register(EventHandler.EventType.WeapondHitEvent, HandleHit);
        EventHandler.Instance.Register(EventHandler.EventType.ParticleEvent, HandleParticleSpawn);
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
                var revEffect = Instantiate(revParticleEffect, arrowHitLocation.transform.position + aOEyOffset, Quaternion.identity);
                revEffect.Play();
            }
         
            foreach (var item in enemiesInArea)
            {
                var enemyHealth = item.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(arrowScript.AoeDamage, true);
                    var arrowParticleEffect = Instantiate(arrowSpecialParticleEffect, item.transform.position + aOEyOffset, item.transform.rotation);
                    arrowParticleEffect.Play();
                }
            }
        }
    }

    private void HandleHit(BaseEventInfo e)
    {
        var hitEvent = e as AttackHitEventInfo;
        if (hitEvent != null)
        {
            switch (hitEvent.WeapondsUsed)
            {
                case AttackHitEventInfo.Weapon.Sword:
                    SpawnHitEffect(hitEvent);
                    break;
                case AttackHitEventInfo.Weapon.Bow:
                    Debug.Log("boop");
                    SpawnHitEffect(hitEvent);
                    break;
            }
        }
    }

    private void SpawnHitEffect(AttackHitEventInfo hitEvent)
    {
        //Vector3 particlePos;
        //particlePos = hitEvent.TargetHit.ClosestPointOnBounds(hitEvent.self.transform.position);
        //ParticleSystem part = Instantiate(hitParticelsEffect, particlePos, Quaternion.identity, transform);
        //part.Play();
    }

    private void HandleParticleSpawn(BaseEventInfo e)
    {
        var spawnEvent = e as ParticleSpawnEventInfo;
        if (spawnEvent != null)
        {
            switch (spawnEvent.ParticleTyp)
            {
                case ParticleSpawnEventInfo.Particle.Hearts:
                    PlayHeartParticles(spawnEvent.SpawnPoint);
                    break;
            }
        }
    }
    private void PlayHeartParticles(Vector3 hit)
    {
        ParticleSystem partHeart = Instantiate(hitHeartParticelsEffect, hit, Quaternion.identity, transform);
        partHeart.Play();
    }

    private void OnDrawGizmos()
    {
        
    }
}

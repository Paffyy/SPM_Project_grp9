using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitListener : MonoBehaviour
{
    public LayerMask CollidersToHit;
    public GameObject RevParticleEffect;
    private ParticleSystem partSystem;
    private Vector3 verticalParticlesOffset = new Vector3(0, 0.2f, 0);
    private Vector3 yOffset = new Vector3(0, 1, 0);
    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.ArrowAoeHitEvent, DoAoeArrowAttack);
    }

    private void Start()
    {
        partSystem = GetComponent<ParticleSystem>();
        Register();
    }

    private void DoAoeArrowAttack(BaseEventInfo e)
    {
        var arrowEventInfo = e as ArrowHitEventInfo;
        if (arrowEventInfo != null)
        {
            Transform arrowHitLocation = arrowEventInfo.Arrow.transform;
            Arrow arrowScript = arrowEventInfo.Arrow.GetComponent<Arrow>();
            var enemiesInArea = Manager.Instance.GetAoeHit(arrowHitLocation.position, CollidersToHit, arrowScript.AoeRadius);
            if (!partSystem.isPlaying)
            {
                transform.position = arrowHitLocation.position + verticalParticlesOffset;
                partSystem.Play();
            }
         
            foreach (var item in enemiesInArea)
            {
                var enemyHealth = item.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(arrowScript.AoeDamage, true);
                    var revEffect = Instantiate(RevParticleEffect, item.transform.position + yOffset, item.transform.rotation);
                    Destroy(revEffect, 2.5f);
                }
            }
        }
    }
}

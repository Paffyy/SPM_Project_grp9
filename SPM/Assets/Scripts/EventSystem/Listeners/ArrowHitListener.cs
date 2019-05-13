﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitListener : MonoBehaviour
{
    public LayerMask CollidersToHit;
    public float AoeRadius;
    public int AoeDamage;
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
            Vector3 arrowHitLocation = arrowEventInfo.Arrow.transform.position;
            var enemiesInArea = Manager.Instance.GetAoeHit(arrowHitLocation, CollidersToHit, AoeRadius);
            if (enemiesInArea.Count > 0 && !partSystem.isPlaying)
            {
                transform.position = arrowHitLocation + verticalParticlesOffset;
                partSystem.Play();
            }
            foreach (var item in enemiesInArea)
            {
                var enemyHealth = item.GetComponent<Health>();
                enemyHealth.TakeDamage(AoeDamage,true);
                var revEffect = Instantiate(RevParticleEffect, item.transform.position + yOffset, item.transform.rotation);
                Destroy(revEffect, 3f);
            }
        }
    }
}
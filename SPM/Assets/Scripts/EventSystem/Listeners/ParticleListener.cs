using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleListener : MonoBehaviour
{
    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, SpawnParticles );
    }
    // Start is called before the first frame update
    void Start()
    {
        Register();
    }

    private void SpawnParticles(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if (deathEventInfo != null)
        {
            var color = GetColor(deathEventInfo.GameObject.GetComponent<MeshRenderer>());
            var particleSystem = deathEventInfo.GameObject.GetComponent<ParticleSystem>();
            var rend = deathEventInfo.GameObject.GetComponent<ParticleSystemRenderer>();
            rend.material.color = color;
            particleSystem.Emit(50);
        }
    }

    private Color GetColor(MeshRenderer meshRenderer)
    {
        return meshRenderer.sharedMaterial.color;
    }
}

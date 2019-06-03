using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawnEventInfo : BaseEventInfo
{
    public enum Particle
    {
        Hearts,
    }
    public Particle ParticleTyp;
    public Vector3 SpawnPoint;

    public ParticleSpawnEventInfo(Vector3 spawnPoint, Particle typeOfParticle)
    {
        SpawnPoint = spawnPoint;
        ParticleTyp = typeOfParticle;
    }
}

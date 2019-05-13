using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeFire : RevitalizeGeometry
{
    public ParticleSystem[] ParticleSystems;

    public override void Revitalize(float offset = 0)
    {
        //foreach (var ps in ParticleSystems)
        //{
        //    var main = ps.main;
        //    main.loop = false;
        //}
        GetComponent<Animator>().SetBool("Revitalized", true);
        GetComponent<AreaOfEffect>().enabled = false;
        Destroy(gameObject, 3f);
    }
}

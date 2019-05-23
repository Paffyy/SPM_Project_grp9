using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeFire : RevitalizeGeometry
{
    [SerializeField]
    private ParticleSystem[] particleSystems;

    public override void Revitalize(float offset = 0)
    {
        GetComponent<Animator>().SetBool("Revitalized", true);
        GetComponent<AreaOfEffect>().enabled = false;
        IsRevitalized = true;
        Destroy(gameObject, 3f);
    }
    public override void InstantRevitalize()
    {
        gameObject.SetActive(false);
    }
}

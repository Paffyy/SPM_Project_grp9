using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeBlockade : RevitalizeGeometry
{
    public ParticleSystem[] FireObjects;
    public GameObject[] BlockadeObjects;

    public override void Revitalize(float offset = 0)
    {
        foreach (var fire in FireObjects)
        {
            fire.GetComponent<Fire>().PutOut();
        }
        foreach (var blockadeObject in BlockadeObjects)
        {
            blockadeObject.GetComponent<Animator>().SetBool("Revitalized", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeBlockade : RevitalizeGeometry
{
    public Collider BlockCollider;
    public Collider ObjectCollider;

    public override void Revitalize(float offset = 0)
    {
        GetComponent<Animator>().SetBool("Revitalized", true);
        Destroy(gameObject, 10f);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void DisableCollider()
    {
        if(BlockCollider != null && BlockCollider.enabled)
        {
            BlockCollider.enabled = false;
        }
        ObjectCollider.enabled = false;
    }
}

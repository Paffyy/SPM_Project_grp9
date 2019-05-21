using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeGround : RevitalizeGeometry
{
    [Range(1, 8)]
    [SerializeField]
    private int brushSize;
    [SerializeField]
    private bool shouldRevitalizeOnStart;
    [SerializeField]
    private RevitalizeTerrain terrain;
    public override void Revitalize(float offset = 0)
    {
        StartCoroutine(RevitalizeOverTime());
    }
    IEnumerator RevitalizeOverTime()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            terrain.RevitalizeArea(gameObject, brushSize, 0.2f);
        }
    }
    void Start()
    {
        if (shouldRevitalizeOnStart)
        {
            Revitalize();
        }
    }

    public override void InstantRevitalize()
    {
        terrain.RevitalizeArea(gameObject, brushSize);
    }
}

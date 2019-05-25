using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnGround : RevitalizeGeometry
{
    [Range(1, 8)]
    [SerializeField]
    private int brushSize;
    [SerializeField]
    private bool shouldRevitalizeOnStart;
    [SerializeField]
    private RevitalizeTerrainSplat terrain;

    public override void Revitalize(float offset = 0)
    {
        StartCoroutine(RevitalizeOverTime());
    }
    IEnumerator RevitalizeOverTime()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            terrain.RevitalizeArea(gameObject, brushSize, Color.red, 0.2f);
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
        terrain.RevitalizeArea(gameObject, brushSize, Color.red);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            terrain.DrawWithMouse2(Color.red,1);
        }
        if (Input.GetKey(KeyCode.T))
        {
            terrain.DrawWithMouse(Color.red, 1);
        }
        if (Input.GetKey(KeyCode.G))
        {
            terrain.DrawWithMouse2(Color.green,5,0.01f);
        }
        if (Input.GetKey(KeyCode.H))
        {
            terrain.DrawWithMouse(Color.white, 5, 1f);
        }
    }
}

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
    private Camera cam;
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
            terrain.RevitalizeArea(gameObject, Color.red, brushSize,  0.2f);
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
        terrain.RevitalizeArea(gameObject, Color.red, brushSize);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            terrain.DrawWithMouse2(cam, Color.red, 1, 0.1f);
        }
        if (Input.GetKey(KeyCode.T))
        {
            terrain.DrawWithMouse(cam, Color.red, 1, 0.1f);
        }
        if (Input.GetKey(KeyCode.G))
        {
            terrain.DrawWithMouse2(cam,Color.green,1, 0.1f);
        }
        if (Input.GetKey(KeyCode.H))
        {
            terrain.DrawWithMouse(cam,Color.white, 1, 0.1f);
        }
    }
}

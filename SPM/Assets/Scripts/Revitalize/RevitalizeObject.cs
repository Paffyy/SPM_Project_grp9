using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeObject : RevitalizeGeometry
{
    [SerializeField]
    [Range(1,10)]
    private float timeToRevitalize;
    [SerializeField]
    [Range(10,40)]
    private int lerpLevels;
    private Material material;
    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }
    public override void Revitalize(float offset = 0)
    {
        StartCoroutine(LerpMaterialOverTime());
        IsRevitalized = true;
    }
    IEnumerator LerpMaterialOverTime()
    {
        for (int i = 1; i < lerpLevels+1; i++)
        {
            float revitalizeFactor = i / (float)lerpLevels;
            yield return new WaitForSeconds(timeToRevitalize / lerpLevels);
            material.SetFloat("_RevitalizeFactor", revitalizeFactor);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Revitalize();
        }
    }
}


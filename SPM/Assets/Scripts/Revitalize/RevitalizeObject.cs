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
    [Tooltip("Defines how many different levels of fading for the revitalize effect")]
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
    public override void InstantRevitalize()
    {
        material.SetFloat("_RevitalizeFactor", 1);
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
    public override void Devitalize(float offset = 0)
    {
        StartCoroutine(LerpMaterialOverTimeInverse(offset));
    }
    IEnumerator LerpMaterialOverTimeInverse(float revitalizeDuration)
    {
        for (int i = 1; i < lerpLevels + 1; i++)
        {
            float revitalizeFactor = 1 - i / (float)lerpLevels;
            yield return new WaitForSeconds(revitalizeDuration / lerpLevels);
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


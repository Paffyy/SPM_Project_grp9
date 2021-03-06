﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Obsolete]
public class RevitalizeVegetation : RevitalizeGeometry
{
    public Material RevitalizedMaterial;
    public float Interval;
    public bool AlphaFading;
    private Renderer rend;
    private List<Color> colList = new List<Color>();
    private Color defaultColor;
    void Awake()
    {
        if (GetComponent<Renderer>() != null)
        {
            rend = GetComponent<Renderer>();
            rend.enabled = true;
        }
        colList = GetColors(RevitalizedMaterial.color, rend.material.color);
    }
    public override void Revitalize(float offset = 0)
    {
        StartCoroutine(ApplyColorOverTime(Interval + offset, colList));
        IsRevitalized = true;
    }

    public override void InstantRevitalize()
    {
        rend.material.color = RevitalizedMaterial.color;
    }

    public void DullMaterial(float offset = 0)
    {
        if (colList.Count > 0)
        {
            var revList = new List<Color>();
            for (int i = colList.Count - 1; i >= 0; i--)
            {
                revList.Add(colList[i]);
            }
            StartCoroutine(ApplyColorOverTime(Interval + offset, revList));
        }
        IsRevitalized = false;
    }
    IEnumerator ApplyColorOverTime(float time, List<Color> colList)
    {
        for (int i = 0; i < colList.Count; i++)
        {
            yield return new WaitForSecondsRealtime(time + UnityEngine.Random.Range(-Interval/2, Interval/8f));
            rend.material.color = colList[i];
        }
    }

    private Color GetColorWithAlpha(Color col , int i = 0) => new Color(col.r, col.g, col.b);

    private List<Color> GetColors(Color a, Color b)
    {
        var colList = new List<Color>();
        colList.Add(new Color(b.r,b.g,b.b,0f));
        if (!AlphaFading)
        {
            for (int i = 1; i < 10; i++)
            {
                var rDelta = ((a.r - b.r) / 10f) * i;
                var gDelta = ((a.g - b.g) / 10f) * i;
                var bDelta = ((a.b - b.b) / 10f) * i;
                colList.Add(new Color(b.r + rDelta, b.g + gDelta, b.b + bDelta));
            }
        }
        else
        {
            for (int i = 1; i < 10; i++)
            {
                var rDelta = ((a.r - b.r) / 10f) * i;
                var gDelta = ((a.g - b.g) / 10f) * i;
                var bDelta = ((a.b - b.b) / 10f) * i;
                colList.Add(new Color(b.r + rDelta, b.g + gDelta, b.b + bDelta, i / 10f));
            }
        }
        colList.Add(a);
        return colList;
    }
}

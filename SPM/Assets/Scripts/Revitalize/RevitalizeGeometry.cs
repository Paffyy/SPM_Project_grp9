using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RevitalizeGeometry : StateMachine
{
    public Material RevitalizedMaterial;
    public float Interval;
    public bool AlphaFading;
    private Renderer rend;
    protected override void Awake()
    {
        base.Awake();
        if (GetComponent<Renderer>() != null)
        {
            rend = GetComponent<Renderer>();
            rend.enabled = true;
        }
    }
    public void Revitalize()
    {
        var colList = GetColors(RevitalizedMaterial.color, rend.material.color);
        StartCoroutine(ApplyColorOverTime(Interval, colList));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeZone : MonoBehaviour
{
    public List<GameObject> Objectives;
    private float timer;
    private bool shouldRevitalize;
    private bool hasRevitalized;
    void Start()
    {
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasRevitalized)
        {
            if (shouldRevitalize)
            {
                RevitalizeTheZone();
                hasRevitalized = true;
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Debug.Log(shouldRevitalize);
                    shouldRevitalize = true;
                    foreach (var item in Objectives)
                    {
                        var revObjective = item.GetComponent<RevitalizeObjective>();
                        if (revObjective != null)
                        {
                            shouldRevitalize = shouldRevitalize && revObjective.IsCompleted;
                        }
                        else
                        {
                            shouldRevitalize = shouldRevitalize && true;
                        }
                    }
                    timer = 1;
                }
            }
        }
    }
    void RevitalizeTheZone()
    {
        foreach (Transform child in transform)
        {
            var rev = child.GetComponent<RevitalizeGeometry>();
            if (rev != null)
            {
                rev.Revitalize(0.7f);
            }
            else
            {
                foreach (Transform grandchildren in child)
                {
                    var rev2 = grandchildren.GetComponent<RevitalizeGeometry>();
                    if (rev2 != null)
                    {
                        rev2.Revitalize(0.7f);
                    }
                }
            }
        }
    }
}

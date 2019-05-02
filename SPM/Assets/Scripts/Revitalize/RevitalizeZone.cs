using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeZone : MonoBehaviour
{
    public List<Enemy> Enemies;
    public bool AllEnemiesDead;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (AllEnemiesDead)
        {
            RevitalizeTheZone();
        }
    }
    void RevitalizeTheZone()
    {
        foreach (Transform child in transform)
        {
            var rev = child.GetComponent<RevitalizeGeometry>();
            if (rev != null)
            {
                rev.Revitalize(1);
            }
            else
            {
                foreach (Transform grandchildren in child)
                {
                    var rev2 = grandchildren.GetComponent<RevitalizeGeometry>();
                    if (rev2 != null)
                    {
                        rev2.Revitalize(1);
                    }
                }
            }
        }
    }
}

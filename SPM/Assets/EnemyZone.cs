using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public List<BaseEnemy> Enemies { get; set; }

    void Start()
    {
        Enemies = GetObjectsInChildren();
    }
    private List<BaseEnemy> GetObjectsInChildren()
    {
        List<BaseEnemy> objects = new List<BaseEnemy>();
        foreach (Transform child in transform)
        {
            BaseEnemy childEnemy = child.GetComponent<BaseEnemy>();
            if (childEnemy != null)
            {
                objects.Add(childEnemy);
            }
            foreach (Transform grandchildren in child)
            {
                BaseEnemy grandChildEnemy = grandchildren.GetComponent<BaseEnemy>();
                if (grandChildEnemy != null)
                {
                    objects.Add(grandChildEnemy);
                }
            }
        }
        return objects;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyManager : MonoBehaviour
{
    public BaseEnemy[] BaseEnemyArray;

    public void Awake()
    {
        BaseEnemyArray = GetComponentsInChildren<BaseEnemy>();
    }

    public void Update()
    {
        
    }
}

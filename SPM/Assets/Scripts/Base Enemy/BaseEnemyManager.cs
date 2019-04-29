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

    public int CountAttackingEnemy()
    {
        int count = 0;
        foreach (BaseEnemy enemy in BaseEnemyArray)
        {
            if (enemy.currectState.GetType() == typeof(BaseEnemyAttackState))
                count++;
        }
        return count;
    }
}

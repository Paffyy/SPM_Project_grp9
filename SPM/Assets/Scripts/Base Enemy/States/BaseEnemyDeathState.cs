using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/DeathSate")]
public class BaseEnemyDeathState : BaseEnemyBaseState
{
    public override void Enter()
    {
        Debug.Log("Dead");
        base.Enter();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/IdleState")]
public class IdleState : EnemyBaseState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        Debug.Log("Idle");
        if (IsAggroed())
        {
            owner.Transition<EAttackState>();
        }
    }
}

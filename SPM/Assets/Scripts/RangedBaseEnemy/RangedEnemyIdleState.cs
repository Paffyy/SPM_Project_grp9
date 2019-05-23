using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RangedBaseEnemy/IdleState")]
public class RangedEnemyIdleState : RangedEnemyBaseState
{
    public override void Enter()
    {
        //idle anim
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if(owner.Fow.TargetsInFieldOfView() != null)
        {
            owner.Transition<RangedEnemyAttackState>();
        }
    }
}

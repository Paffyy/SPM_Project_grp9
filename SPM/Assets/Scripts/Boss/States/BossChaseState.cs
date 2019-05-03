using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/ChaseState")]

public class BossChaseState : BaseEnemyBaseState
{
    //[SerializeField] private float attackDistance;
    //[SerializeField] private float lostTargetDistance;

    public override void Enter()
    {
        base.Enter();
        owner.currectState = this;
    }

    public override void HandleUpdate()
    {
        owner.NavAgent.SetDestination(owner.player.transform.position);



        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance)
            owner.Transition<BossAttackState>();

        base.HandleUpdate();
    }
}

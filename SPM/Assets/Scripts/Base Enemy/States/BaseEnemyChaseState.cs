﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/ChaseState")]

public class BaseEnemyChaseState : BaseEnemyBaseState
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
        owner.UpdateDestination(owner.player.transform.position);
        //Debug.Log("chaseState");

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > lostTargetDistance)
            owner.Transition<BaseEnemyPatrolState>();

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance)
            owner.Transition<BaseEnemyAttackState>();

        base.HandleUpdate();
    }
}

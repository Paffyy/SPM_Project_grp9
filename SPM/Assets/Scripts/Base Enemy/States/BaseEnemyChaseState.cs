using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/ChaseState")]

public class BaseEnemyChaseState : BaseEnemyBaseState
{
    //[SerializeField] private float attackDistance;
    //[SerializeField] private float lostTargetDistance;

    public override void Enter()
    {
        //Debug.Log("chaseState");
        base.Enter();
        speedModifier = 1.5f;
    }

    public override void HandleUpdate()
    {
        UpdateDestination(owner.player.transform.position);
        UpdateRotation(owner.player.transform);


        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > lostTargetDistance)
            owner.Transition<BaseEnemyPatrolState>();

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance)
            owner.Transition<BaseEnemyAttackState>();

        base.HandleUpdate();
    }
}

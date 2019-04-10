using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/ChaseState")]

public class BaseEnemyChaseState : BaseEnemyBaseState
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float lostTargetDistance;


    public override void HandleUpdate()
    {
        owner.NavAgent.SetDestination(owner.player.transform.position);


        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > lostTargetDistance)
            owner.Transition<BaseEnemyPatrolState>();
    }
}

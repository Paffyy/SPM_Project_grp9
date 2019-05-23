using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ranged/ChaseState")]

public class RangedChaseState : RangedBaseState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        owner.NavAgent.SetDestination(owner.player.transform.position);
        UpdateRotation(owner.player.transform);
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < owner.attackDistance)
            owner.Transition<FiresOfHeavenState>();
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > owner.lostTargetDistance)
            owner.Transition<RangedEnemyPatrolState>();
        base.HandleUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RangedBaseEnemy/IdleState")]
public class RangedEnemyIdleState : RangedEnemyBaseState
{

    [SerializeField] private Vector3[] patrolPoints;
    private float pointSize = 1.1f;
    private int currentPoint = 0;

    public override void Enter()
    {
        base.Enter();
        if (owner.NavAgent.enabled)
            owner.NavAgent.isStopped = false;
        ClosestPoint();
    }

    public override void HandleUpdate()
    {
        UpdateDestination(owner.Path.PathObjects[currentPoint].position);
        UpdateRotation(owner.Path.PathObjects[currentPoint]);
        SetDestination();
        if (owner.Fow.TargetsInFieldOfView() != null || Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearRadius)
        {
            owner.Transition<RangedEnemyAttackState>();
        }
        base.HandleUpdate();
    }

    private void ClosestPoint()
    {

        int closest = 0;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float dist = Vector3.Distance(owner.transform.position, patrolPoints[i]);
            if (dist < Vector3.Distance(owner.transform.position, patrolPoints[closest]))
                closest = i;
        }
        currentPoint = closest;
    }
}

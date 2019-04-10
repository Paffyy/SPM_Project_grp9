using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/PatrolState")]
public class BaseEnemyPatrolState : BaseEnemyBaseState
{
    [SerializeField] private Vector3[] patrolPoints;
    [SerializeField] private float chaseDistance;
    //[SerializeField] private float hearingRange;
    private int currentPoint = 0;

    public override void Enter()
    {
        base.Enter();
        ClosestPoint();
    }

    public override void HandleUpdate()
    {
        owner.NavAgent.SetDestination(patrolPoints[currentPoint]);
        if (Vector3.Distance(owner.transform.position, patrolPoints[currentPoint]) < 1)
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        if (CanHearPlayer())
        {

        }

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

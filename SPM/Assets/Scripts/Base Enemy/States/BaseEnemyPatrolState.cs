using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/PatrolState")]
public class BaseEnemyPatrolState : BaseEnemyBaseState
{
    [SerializeField] private Vector3[] patrolPoints;
    [SerializeField] private float chaseDistance;
    //avståndet som fienden behöver vara från punkten för att gå till nästa
    public float pointSize;
    //[SerializeField] private float hearingRange;
    public int currentPoint = 0;

    public override void Enter()
    {
        base.Enter();
        ClosestPoint();
        Debug.Log("PatrolState");
    }

    public override void HandleUpdate()
    {
        owner.NavAgent.SetDestination(owner.Path.PathObjects[currentPoint].position);
        if (Vector3.Distance(owner.transform.position, owner.Path.PathObjects[currentPoint].position) < 1 )
        {
            currentPoint = (currentPoint + 1) % owner.Path.PathObjects.Count;
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < chaseDistance)
            owner.Transition<BaseEnemyChaseState>();


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

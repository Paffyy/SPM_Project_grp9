using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/PatrolState")]
public class BaseEnemyPatrolState : BaseEnemyBaseState
{
    [SerializeField] private Vector3[] patrolPoints;
    //[SerializeField] private float chaseDistance;

    //avståndet som fienden behöver vara från punkten för att gå till nästa
    private float pointSize = 2.0f;
    //[SerializeField] private float hearingRange;
    public int currentPoint = 0;

    public override void Enter()
    {
        base.Enter();
        ClosestPoint();
        //Debug.Log("PatrolState");
    }

    public override void HandleUpdate()
    {
        Vector3 dis = owner.player.transform.position - owner.transform.position;
        float distance = dis.sqrMagnitude;

        UpdateDestination(owner.Path.PathObjects[currentPoint].position);
        UpdateRotation(owner.Path.PathObjects[currentPoint]);

        Vector3 toPoint = owner.Path.PathObjects[currentPoint].position - owner.transform.position;
        float distanceToPoint = toPoint.sqrMagnitude;

        if (distanceToPoint < pointSize * pointSize)
        {
            if(isWaitAtPosition == true)
            {
                owner.WaitAtPosition(waitAtPatrolPoints);

            }
            currentPoint = (currentPoint + 1) % owner.Path.PathObjects.Count;
        }

        if(distance < owner.Fow.viewRadius * owner.Fow.viewRadius)
        {
            if (distance < hearRadius * hearRadius || owner.Fow.TargetsInFieldOfView() != null)
            {
                //Debug.Log(owner.Fow.TargetsInFieldOfView().ToString());
                owner.Transition<BaseEnemyChaseState>();
            }
        }



        base.HandleUpdate();

    }

    private void ClosestPoint()
    {
        
        int closest = 0;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Vector3 dis = owner.Path.PathObjects[i].position - owner.transform.position;
            float distance = dis.sqrMagnitude;

            Vector3 disClosest = owner.Path.PathObjects[closest].position - owner.transform.position;
            float distanceClosest = disClosest.sqrMagnitude;

            if (distance < distanceClosest)
                closest = i;
        }
        currentPoint = closest;
    }
}

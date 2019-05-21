﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ranged/PatrolState1")]
public class RangedEnemyPatrolState : RangedBaseState
{
    [SerializeField] private Vector3[] patrolPoints;
    //[SerializeField] private float chaseDistance;

    //avståndet som fienden behöver vara från punkten för att gå till nästa
    private float pointSize = 1.1f;
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
        UpdateDestination(owner.Path.PathObjects[currentPoint].position);
        UpdateRotation(owner.Path.PathObjects[currentPoint]);
        if (Vector3.Distance(owner.transform.position, owner.Path.PathObjects[currentPoint].position) < pointSize )
        {
            currentPoint = (currentPoint + 1) % owner.Path.PathObjects.Count;
        }

        //if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < chaseDistance)
        //if(owner.Fow.TargetsInFieldOfView() != null && Vector3.Distance(owner.player.transform.position, owner.transform.position) < chaseDistance)
        if (owner.Fow.TargetsInFieldOfView() != null
            || Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearRadius)
        {
            //Debug.Log(owner.Fow.TargetsInFieldOfView().ToString());
            owner.Transition<RangedChaseState>();
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
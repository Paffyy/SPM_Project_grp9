using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/BackOffState")]
public class BaseEnemyBackOffState : BaseEnemyBaseState
{
    public float BackTime;
    public float Speed;
    public float MaxDistance;
    public float SpeedIncreas;

    private float timer;
    private float minDistance = 1.0f;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Back Off State");
        owner.NavAgent.updateRotation = false;
        owner.NavAgent.speed += SpeedIncreas;
        //owner.StartCoroutine(BackOff());
        timer = BackTime;
    }

    public override void HandleUpdate()
    {
        timer -= Time.deltaTime;

        owner.transform.LookAt(owner.transform.position);
        UpdateDestination(owner.player.transform.position - owner.transform.forward * Speed);
        float dist = Vector3.Distance(owner.player.transform.position, owner.transform.position);
        if (  timer < 0 || dist > MaxDistance
            || dist < minDistance)
        {
            int rand = Random.Range(1, 5);
            if (rand == 1);
                owner.Transition<BaseEnemyCircleState>();
            //owner.Transition<BaseEnemyAttackState>();
        }
        base.HandleUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        owner.NavAgent.speed -= SpeedIncreas;
    }
}

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
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Back Off State");
        owner.NavAgent.updateRotation = false;
        owner.NavAgent.speed += SpeedIncreas;
        owner.StartCoroutine(BackOff());
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        //owner.transform.RotateAround(Vector3.zero, Vector3.up, 10 * Time.deltaTime);
        //Quaternion direction = (owner.player.transform.position - owner.transform.position).normalized;
        //owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, owner.player.transform.position, Time.deltaTime * 10);
        owner.transform.LookAt(owner.transform.position);
        owner.UpdateDestination(owner.player.transform.position - owner.transform.forward * Speed, 2f);
        if(Vector3.Distance(owner.player.transform.position, owner.transform.position) > MaxDistance)
        {
            owner.Transition<BaseEnemyCircleState>();
            //owner.Transition<BaseEnemyAttackState>();
        }
    }

    private IEnumerator BackOff()
    {
        yield return new WaitForSeconds(BackTime);
        //owner.Transition<BaseEnemyAttackState>();
        owner.Transition<BaseEnemyCircleState>();
    }

    public override void Exit()
    {
        base.Exit();
        owner.NavAgent.speed -= SpeedIncreas;
    }
}

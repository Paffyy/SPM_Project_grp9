using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/AttackState")]
public class BaseEnemyAttackState : BaseEnemyBaseState
{

    

    private float currentCooldown;

    public override void Enter()
    {
        //Debug.Log("AttackState");
        base.Enter();
        owner.MeshRen.material.color = Color.red;
        currentCooldown = 0.2f;
        //hitta hur många andra fiender som är i AttackState
        BaseEnemy.NumberOfEnemiesInAttackState++;
        if (BaseEnemy.NumberOfEnemiesInAttackState > 2)
        {
            owner.Transition<BaseEnemyCircleState>();
        }
        speedModifier = 1.25f;

    }

    public override void Exit()
    {
        BaseEnemy.NumberOfEnemiesInAttackState--;
        base.Exit();
    }
    public override void HandleUpdate()
    {
        //owner.NavAgent.SetDestination(owner.player.transform.position);
        UpdateDestination(owner.player.transform.position);
        UpdateRotation(owner.player.transform);

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < PlacmentDistance)
        {
            Attack();
            if (Random.Range(0, 3) == 0)
            {
                owner.Transition<BaseEnemyBackOffState>();
            }
        }
        currentCooldown -= Time.deltaTime;
        //tittar på spelaren

        //owner.transform.LookAt(owner.player.transform.position);

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > chaseDistance)
            owner.Transition<BaseEnemyChaseState>();

        base.HandleUpdate();
    }

    //void LookAtTarget(Transform target)
    //{
    //    Vector3 dir = (target.position - owner.transform.position).normalized;
    //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
    //    owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, lookRotation, 5f);
    //}

    private void Attack()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0)
            return;

        //Skadar spelarn
        GameObject[] arr = owner.Fow.TargetsInFieldOfView();
        if (arr != null)
        {
                //Debug.Log(arr[0]);
                PlayerHealth player = arr[0].GetComponent<PlayerHealth>();
            //if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < owner.attackDistance)
            //{
                Vector3 push = (((owner.player.transform.position) - owner.transform.position).normalized + Vector3.up * 2) * 4;
                player.TakeDamage(owner.Damage, push, owner.transform.position);
            //}

        }
        //arr[0].GetComponent<Player>.Hit();
        currentCooldown = cooldown;

    }

}

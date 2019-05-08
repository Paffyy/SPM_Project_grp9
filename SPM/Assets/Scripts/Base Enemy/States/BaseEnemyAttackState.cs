using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/AttackState")]
public class BaseEnemyAttackState : BaseEnemyBaseState
{

    private float currentCooldown;
    private bool backOff = false;
    private bool toPlayer = false;
    private float backTimer = 5.0f;
    private float timer;

    private float circleDistance = 1.5f;

    public override void Enter()
    {
        //Debug.Log("AttackState");
        base.Enter();
        owner.MeshRen.material.color = Color.red;
        currentCooldown = cooldown;
        owner.currectState = this;
        timer = backTimer;
        //hitta hur många andra fiender som är i AttackState
    }

    public override void HandleUpdate()
    {

        //if(Vector3.Distance(owner.transform.position, owner.player.transform.position) < PlacmentDistance)
        //{
        //    owner.NavAgent.isStopped = true;
        //    Attack();
        //}
        //else{
        //    owner.NavAgent.isStopped = false;
        //    owner.NavAgent.SetDestination(owner.player.transform.position);
        //}
        //owner.NavAgent.SetDestination(owner.player.transform.position);

        if (backOff)
        {
            BackOff();
            timer -= Time.deltaTime;
        }

        if(backOff && Vector3.Distance(owner.transform.position, owner.player.transform.position) < circleDistance || timer < 0)
        {
            owner.Transition<BaseEnemyCircleState>();
            backOff = false;
            timer = backTimer;
            toPlayer = true;
        }
        if (toPlayer)
        {
            if(Vector3.Distance(owner.transform.position, owner.player.transform.position) > 1f)
            {
                owner.UpdateDestination(owner.player.transform.position, 0.1f);
            }
        }

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < PlacmentDistance)
        {
            Attack();
            backOff = true;
        }





        //tittar på spelaren
        LookAtTarget(owner.player.transform);
        //owner.transform.LookAt(owner.player.transform.position);

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > chaseDistance)
            owner.Transition<BaseEnemyChaseState>();

        base.HandleUpdate();
    }

    void LookAtTarget(Transform target)
    {
        Vector3 dir = (target.position - owner.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, lookRotation, 5f);
    }

    void BackOff()
    {
        owner.NavAgent.updateRotation = false;
        //owner.transform.LookAt(owner.player.transform);
        owner.UpdateDestination(-owner.transform.forward * 10, 2f);
        //owner.NavAgent.updateRotation = true;
        //Debug.Log("back off");
    }

    private void Attack()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0)
            return;

        //Skadar spelarn
        GameObject[] arr = owner.Fow.TargetsInFieldOfView();
        if(arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Debug.Log(arr[i]);
                PlayerHealth player = arr[i].GetComponent<PlayerHealth>();
                Vector3 push = (((player.transform.position) - owner.transform.position).normalized + Vector3.up * 2) * 4;
                player.TakeDamage(owner.Damage, push, owner.transform.position);
            }
        }
        //arr[0].GetComponent<Player>.Hit();
        currentCooldown = cooldown;

    }

}

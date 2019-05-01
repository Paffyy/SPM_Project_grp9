using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/AttackState")]
public class BaseEnemyAttackState : BaseEnemyBaseState
{

    //[SerializeField] private float chaseDistance;
    //[SerializeField] private float cooldown;
    //public float PlacmentDistance;

    private float currentCooldown;
    private bool backOff = false;
    private bool toPlayer = false;
    private bool circle = false;
    private float backTimer = 2.0f;
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
        Debug.Log("back off " + backOff);
        Debug.Log("cicle player " + circle);
        Debug.Log("to player " + toPlayer);
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
        //if(timer < 0)
        //{
        //    backOff = false;
        //    toPlayer = true;
        //    timer = backTimer;
        //}
        if(backOff && Vector3.Distance(owner.transform.position, owner.player.transform.position) < circleDistance || timer < 0)
        {
            backOff = false;
            timer = backTimer;
            //circle = true;
            toPlayer = true;
        }
        if (toPlayer)
        {
            owner.UpdateDestination(owner.player.transform.position, 0.1f);
        }
        //if (circle && timer < 0)
        //{
        //    timer -= Time.deltaTime;
        //    CirclePlayer();
        //}

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



    void CirclePlayer()
    {
        float angleDiviation = 1.5f;
        float currentDistance = Vector3.Distance(owner.transform.position, owner.player.transform.position);
        float currentAngle = Vector3.Angle(owner.player.transform.position, owner.transform.position);
        float randomAgle = Random.Range(-angleDiviation, angleDiviation);

        Vector3 position = owner.player.transform.forward * currentDistance;

        float x = owner.player.transform.position.x + currentDistance * Mathf.Cos(currentAngle + randomAgle);
        float y = owner.player.transform.position.y + currentDistance * Mathf.Sin(currentAngle + randomAgle);

        owner.UpdateDestination(new Vector3(x, y, owner.transform.position.z), 0.1f);
    }

    private void Attack()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0)
            return;

        //Skadar spelarn
        GameObject[] arr = owner.Fow.TargetsInFieldOfView();
            for (int i = 0; i < arr.Length; i++)
            {
                Debug.Log(arr[i]);
                arr[i].GetComponent<PlayerHealth>().TakeDamage(owner.Damage);
            }
        //arr[0].GetComponent<Player>.Hit();
        currentCooldown = cooldown;

    }

}

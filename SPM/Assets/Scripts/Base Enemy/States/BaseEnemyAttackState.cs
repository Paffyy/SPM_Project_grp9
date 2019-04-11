using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/AttackState")]
public class BaseEnemyAttackState : BaseEnemyBaseState
{

    //[SerializeField] private float chaseDistance;
    //[SerializeField] private float cooldown;
    public float PlacmentDistance;

    private float currentCooldown;

    public override void Enter()
    {
        Debug.Log("AttackState");
        base.Enter();
        owner.MeshRen.material.color = Color.red;
        currentCooldown = cooldown;
        //hitta hur många andra fiender som är i AttackState
    }

    public override void HandleUpdate()
    {
        owner.NavAgent.SetDestination(owner.player.transform.position);
        if(Vector3.Distance(owner.transform.position, owner.player.transform.position) < PlacmentDistance)
        {
            Attack();
            //owner.NavAgent.isStopped = true;
        }

        //tittar på spelaren
        owner.transform.LookAt(owner.player.transform.position);

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > chaseDistance)
            owner.Transition<BaseEnemyChaseState>();

        base.HandleUpdate();
    }

    void BackOff()
    {

    }

    void CirclePlayer()
    {

    }

    private void Attack()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0)
            return;

        //Skadar spelarn
        GameObject[] arr = owner.Fow.TargetsInFieldOfView();
        if (arr != null)
        {
            //arr[0].GetComponent<Player>.Hit();
        } 

         currentCooldown = cooldown;
    }
}

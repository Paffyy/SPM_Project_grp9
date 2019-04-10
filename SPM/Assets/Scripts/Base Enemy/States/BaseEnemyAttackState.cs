using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/AttackState")]
public class BaseEnemyAttackState : BaseEnemyBaseState
{

    [SerializeField] private float chaseDistance;
    [SerializeField] private float cooldown;
    public float PlacmentDistance;

    private float currentCooldown;

    public override void Enter()
    {
        base.Enter();
        owner.MeshRen.material.color = Color.red;
        currentCooldown = cooldown;
        //hitta hur många andra fiender som är i AttackState
    }

    public override void HandleUpdate()
    {
        owner.NavAgent.SetDestination(owner.player.transform.position - new Vector3(-PlacmentDistance, -PlacmentDistance, 0));
        Attack();

        //if (!canseeplayer())
        //    owner.transition<alertstate>();
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > chaseDistance)
            owner.Transition<BaseEnemyChaseState>();
        //if (Input.GetKeyDown(KeyCode.Space))
        //    owner.Transition<FleeState>();
    }

    private void Attack()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0)
            return;

        if (Vector3.Dot(owner.transform.position, owner.player.transform.position) < -0.6f && CanSeePlayer() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < owner.AttackRange)
            //Spelare tar skada

         //owner.transform.position += -owner.player.transform.position.normalized 

            currentCooldown = cooldown;
    }
}

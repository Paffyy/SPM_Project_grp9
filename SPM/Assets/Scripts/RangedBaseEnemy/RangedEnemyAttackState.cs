using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RangedBaseEnemy/AttackState")]
public class RangedEnemyAttackState : RangedEnemyBaseState
{
    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    private float attackCoolDown;
    private float coolDownTimer;
    private bool isUsingRightHand;

    public override void Enter()
    {
        owner.Anim.SetBool("IsAttacking", true);
        if(owner.NavAgent.enabled)
            owner.NavAgent.isStopped = true;
        //owner.NavAgent.updatePosition = false;
        coolDownTimer = attackCoolDown;
        base.Enter();
    }

    public override void Exit()
    {
        owner.Anim.SetBool("IsAttacking", false);
        base.Exit();
    }

    public override void HandleUpdate()
    {
        UpdateRotation(owner.player.transform);
        if (coolDownTimer <= 0)
        {
            ShootFireBall();
        } else
        {
            coolDownTimer -= Time.deltaTime;
        }
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > owner.lostTargetDistance)
        {
            owner.Transition<RangedEnemyIdleState>();
        }
        base.HandleUpdate();
    }

    private void ShootFireBall()
    {
        isUsingRightHand = !isUsingRightHand;
        if (isUsingRightHand)
        {
            owner.Anim.SetBool("IsUsingRightHand", true);
        } else
        {
            owner.Anim.SetBool("IsUsingRightHand", false);
        }
        coolDownTimer = attackCoolDown;
    }
}

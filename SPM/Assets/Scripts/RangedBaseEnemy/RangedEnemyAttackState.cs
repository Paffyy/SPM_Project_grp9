using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RangedBaseEnemy/AttackState")]
public class RangedEnemyAttackState : RangedEnemyBaseState
{

    private bool hasUsedRightHand;

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        UpdateRotation(owner.player.transform);
        if (CoolDownManager.Instance.RangedEnemyAttackOnCoolDown == false)
        {
            ShootFireBall();
        }
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > owner.lostTargetDistance)
        {
            owner.Transition<RangedEnemyIdleState>();
        }
        base.HandleUpdate();
    }

    private void ShootFireBall()
    {
        hasUsedRightHand = !hasUsedRightHand;
        GameObject fireBallClone;
        if (hasUsedRightHand == true)
        {
            fireBallClone = Instantiate(owner.FireBall, owner.LeftHand.transform.position, owner.transform.rotation);
        }
        else
        {
            fireBallClone = Instantiate(owner.FireBall, owner.RightHand.transform.position, owner.transform.rotation);
        }
        fireBallClone.GetComponent<Projectile>().Velocity = fireBallClone.GetComponent<Projectile>().Speed * (owner.player.transform.position - fireBallClone.transform.position).normalized;
        CoolDownManager.Instance.StartRangedEnemyAttackCoolDown(cooldown);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/JumpSideState")]
public class JumpSideState : BaseEnemyBaseState
{
    private float jumpAngle;

    public override void Enter()
    {
        jumpAngle = Random.Range(-90, 90);
        Debug.Log("JumpSideState");
        base.Enter();
    }

    private void Jump()
    {
        float currentAngle = Vector3.Angle(owner.transform.position, owner.player.transform.position);
        float radiants = currentAngle + jumpAngle * Mathf.Deg2Rad;
        controller.Velocity += new Vector3(Mathf.Cos(radiants),0, Mathf.Sin(radiants));
    }

    public override void HandleUpdate()
    {
        UpdateDestination(owner.player.transform.position);
        //Debug.Log("chaseState");

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > lostTargetDistance)
            owner.Transition<BaseEnemyPatrolState>();

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance)
            owner.Transition<BaseEnemyAttackState>();

        base.HandleUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/ForwardShockwaveState")]
public class BossForwardShockwaveState : BaseEnemyBaseState
{
    private bool fired = false;
    public GameObject ShockWaveObject;

    public override void Enter()
    {
        base.Enter();
        owner.currectState = this;
        owner.NavAgent.isStopped = true;

    }

    public override void HandleUpdate()
    {
        

        ShockWaveAttack();
        base.HandleUpdate();

        owner.Transition<BossAttackState>();


    }


    private void ShockWaveAttack()
    {
        owner.StartCoroutine(ShockAndAwe());
        
    }

    private IEnumerator ShockAndAwe()
    {
        owner.NavAgent.isStopped = true;
        yield return new WaitForSeconds(1.2f);
        GameObject.Instantiate(ShockWaveObject, new Vector3(owner.transform.position.x, owner.transform.position.y / 2, owner.transform.position.z),
        owner.transform.rotation);
        yield return new WaitForSeconds(1);
        owner.NavAgent.isStopped = false;

    }

}

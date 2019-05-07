using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/ForwardShockwaveState")]
public class BossForwardShockwaveState : BossBaseState
{
    private bool fired = false;
    public GameObject ShockWaveObject;

    public override void Enter()
    {
        base.Enter();
        owner.currectState = this;
        owner.anim.SetTrigger("ShockWave");
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
        //Y-värdet är så att shockvågen spawnar vid fötterna på bossen + några enheter under marken
        GameObject.Instantiate(ShockWaveObject, new Vector3(owner.transform.position.x, (owner.transform.position.y / 2) - 1f, owner.transform.position.z),
        owner.transform.rotation);
        yield return new WaitForSeconds(1);
        owner.NavAgent.isStopped = false;

    }

}

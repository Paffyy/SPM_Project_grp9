using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/IdleState")]
public class BaseEnemyIdleState : BaseEnemyBaseState
{

    public override void Enter()
    {
        //Debug.Log("IdleState");
        base.Enter();
        owner.MeshRen.material.color = Color.white;
        owner.currectState = this;
    }

    public override void HandleUpdate()
    {

        base.HandleUpdate();
        if (owner.Fow.TargetsInFieldOfView() != null && Physics.Linecast(owner.transform.position, owner.player.transform.position) 
            || Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearRadius)
        {
            //Debug.Log(owner.Fow.TargetsInFieldOfView().ToString());
            owner.Transition<BaseEnemyChaseState>();
        }
    }

}

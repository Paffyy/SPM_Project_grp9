using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/IdleState")]
public class BaseEnemyIdleState : BaseEnemyBaseState
{

    public override void Enter()
    {
        Debug.Log("IdleState");
        base.Enter();
        owner.MeshRen.material.color = Color.white;
    }

    public override void HandleUpdate()
    {

        base.HandleUpdate();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    protected Enemy owner;

    public override void Enter()
    {
    }

    protected bool IsAggroed()
    {
        if (Vector3.Distance(owner.transform.position, owner.Player.transform.position) < 40)
        {
            return true;
        }
        return false;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Enemy)owner;
    }

}

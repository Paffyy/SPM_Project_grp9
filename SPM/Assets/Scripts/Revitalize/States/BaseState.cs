using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : State
{
    protected RevitalizeGeometry owner;
    public override void Initialize(StateMachine owner)
    {
        //this.owner = (RevitalizeGeometry)owner;
    }
    public override void Enter()
    {
        base.Enter();
    }
}

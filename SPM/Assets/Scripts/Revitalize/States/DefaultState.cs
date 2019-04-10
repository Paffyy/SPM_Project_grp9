using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RevGeometry/DefaultState")]
public class DefaultState : BaseState
{
    public override void Enter()
    {
        owner.DullMaterial();
        base.Enter();
    }
    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
            owner.Transition<RevitalizedState>();
    }
}

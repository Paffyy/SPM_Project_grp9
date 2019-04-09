using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "RevGeometry/RevitalizedState")]

public class RevitalizedState : BaseState
{
    public override void Enter()
    {
        owner.Revitalize();
        base.Enter();
    }
    public override void HandleUpdate()
    {
        base.HandleUpdate();

    }
}

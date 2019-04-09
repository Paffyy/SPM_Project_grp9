using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RevGeometry/IdleState")]
public class IdleState : BaseState
{
    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
            owner.Transition<RevitalizedState>();
    }
}

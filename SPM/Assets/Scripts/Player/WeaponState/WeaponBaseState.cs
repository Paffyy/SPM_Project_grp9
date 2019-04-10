using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseState : State
{
    protected Weapon owner;

    public override void Enter() { }
    public override void HandleUpdate() { }
    public override void Initialize(StateMachine owner) {
        this.owner = (Weapon)owner;
    }
}

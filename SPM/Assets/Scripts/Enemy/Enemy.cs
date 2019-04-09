using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StateMachine
{
    public Player Player;
    public Projectile Projectile;

    protected override void Awake()
    {
        base.Awake();
    }
}

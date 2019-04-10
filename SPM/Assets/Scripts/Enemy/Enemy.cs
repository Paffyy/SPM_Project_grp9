using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StateMachine
{
    public GameObject Player;
    public GameObject Projectile;

    protected override void Awake()
    {
        base.Awake();
    }
}

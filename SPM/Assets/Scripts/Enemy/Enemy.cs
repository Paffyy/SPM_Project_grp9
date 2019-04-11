using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StateMachine
{
    public GameObject Player;
    public GameObject Projectile;
    public int Health = 100;

    protected override void Awake()
    {
        base.Awake();
    }
}

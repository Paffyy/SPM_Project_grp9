using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StateMachine
{
    public GameObject Player;
    public GameObject Projectile;
    public int Health = 100;
    public float EnemyID;

    void Start()
    {
        EnemyID = transform.position.sqrMagnitude;
        GameControl.GameController.Enemies.Add(EnemyID, gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}

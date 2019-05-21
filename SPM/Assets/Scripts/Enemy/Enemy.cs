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
        if (!GameControl.GameController.Enemies.ContainsKey(EnemyID))
        {
            GameControl.GameController.Enemies.Add(EnemyID, gameObject);
        }
    }


    protected override void Awake()
    {
        EnemyID = transform.position.sqrMagnitude;
        //GameControl.GameController.Enemies.Add(EnemyID, gameObject);
        base.Awake();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : StateMachine
{
    //för debug
    [HideInInspector] public MeshRenderer MeshRen;


    public PathMaker Path;
    public int Health;
    public int Damage;
    public float IFrameTime;
    [HideInInspector] public float IFrameCoolDown;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public FieldOfView Fow;

    public State currectState;

    [HideInInspector] public float chaseDistance;
    public float cooldown;
    public float attackDistance;
    public float lostTargetDistance;
    public float moveSpeed;
    public float hearRadius;
    public float AttackPlacmentDistance;

    //public float AttackRange;

    public LayerMask visionMask;
    public Player player;

    protected override void Awake()
    {

        //för debug
        MeshRen = GetComponent<MeshRenderer>();

        IFrameCoolDown = IFrameTime;
        //BaseEnemyList.AddLast(this.gameObject);
        NavAgent = GetComponent<NavMeshAgent>();
        Fow = GetComponent<FieldOfView>();
        chaseDistance = Fow.viewRadius;
        base.Awake();
    }

    //private void Update()
    //{
    //    //debug shit
    //    if (Input.GetKeyDown("h"))
    //    {
    //        hit(2, player.transform.position, 20.0f);
    //    }
    //    //debug shit

    //    IFrameCoolDown -= Time.deltaTime;
    //}

    //hitOrigin vectorn används för att putta tillbaka fienden från denna vectorns riktning i förhållande till dennes position
    public bool hit(int dmg, Vector3 hitOrigin, float pushBackDistance)
    {
        if(IFrameCoolDown > 0)
            return false;

        Health -= dmg;
        if(Health <= 0)
        {
            transform.position += (Vector3.up * 0.2f) + (transform.position - hitOrigin).normalized * (pushBackDistance * 3) * Time.deltaTime;
            die();
        }

        //denna pushback grej måste göras bättre
        transform.position += (Vector3.up * 0.2f) + (transform.position - hitOrigin).normalized * pushBackDistance * Time.deltaTime;

        IFrameCoolDown = IFrameTime;
        return true;
    }

    public void die()
    {
        MeshRen.material.color = Color.gray;
        Destroy(gameObject, 1.5f);
    }
}

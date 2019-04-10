using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : StateMachine
{
    //för debug
    [HideInInspector] public MeshRenderer MeshRen;
    [HideInInspector] public Renderer Ren;


    public PathMaker Path;
    [HideInInspector] public LinkedList<GameObject> BaseEnemyList = new LinkedList<GameObject>();
    [HideInInspector] public NavMeshAgent NavAgent;
    public float AttackRange;

    public LayerMask visionMask;
    public Player player;

    protected override void Awake()
    {
        //för debug
        MeshRen = GetComponent<MeshRenderer>();
        Ren = GetComponent<Renderer>();

        BaseEnemyList.AddLast(this.gameObject);
        NavAgent = GetComponent<NavMeshAgent>();
        base.Awake();
    }
}

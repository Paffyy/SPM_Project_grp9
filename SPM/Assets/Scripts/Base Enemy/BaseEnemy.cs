using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : StateMachine
{
    //för debug
    [HideInInspector] public MeshRenderer MeshRen;


    public PathMaker Path;
    //public int Health;
    public int Damage;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public FieldOfView Fow;

    public State currectState;

    public float chaseDistance;
    public float cooldown;
    public float attackDistance;
    public float lostTargetDistance;
    public float moveSpeed;
    public float hearRadius;
    public float AttackPlacmentDistance;
    public bool IsWaitAtPosition = false;
    public float waitAtPatrolPoints = 0.0f;

    private EnemyHealth healthSystem;
    [HideInInspector]public CharacterController controller;

    private float timeBetweenSetDestination = 0.1f;
    private float timer;

    //public float AttackRange;

    public LayerMask visionMask;
    public Player player;

    protected override void Awake()
    {
        //för debug
        MeshRen = GetComponent<MeshRenderer>();

        timer = timeBetweenSetDestination;
        //BaseEnemyList.AddLast(this.gameObject);
        NavAgent = GetComponent<NavMeshAgent>();
        healthSystem = GetComponent<EnemyHealth>();
        Fow = GetComponent<FieldOfView>();
        chaseDistance = Fow.viewRadius;
        controller = GetComponent<CharacterController>();
        if(controller != null)
        {
            controller.enabled = false;
        }
        base.Awake();
    }

    public void UpdateDestination(Vector3 destination)
    {
        //ser till så att fiendens navagent är på asså att den inte är i luften
        if(NavAgent.enabled == true)
        {
            //if(timer < 0)
            //{
                NavAgent.SetDestination(destination);
                //timer = timeBetweenSetDestination;
            //}
        }
    }

    public void WaitAtPosition(float seconds)
    {
        
        StartCoroutine(Wait(seconds));
        
    }


    private IEnumerator Wait(float seconds)
    {
        NavAgent.isStopped = true;
        //idleState
        yield return new WaitForSeconds(seconds);
        NavAgent.isStopped = false;
    }

}

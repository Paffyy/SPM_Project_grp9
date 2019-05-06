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
    public float PushBack;
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
    public float waitAtPatrolPoints = 0.0f;

    [HideInInspector] public EnemyHealth healthSystem;
    [HideInInspector] public CharacterController controller;

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
        healthSystem = GetComponent<EnemyHealth>();
        Fow = GetComponent<FieldOfView>();
        chaseDistance = Fow.viewRadius;
        controller = GetComponent<CharacterController>();
        base.Awake();
    }

    //hitOrigin vectorn används för att putta tillbaka fienden från denna vectorns riktning i förhållande till dennes position
    //public bool hit(int dmg, Vector3 hitOrigin, float pushBackDistance)
    //{
    //    if(IFrameCoolDown > 0)
    //        return false;

    //    Health -= dmg;
    //    if(Health <= 0)
    //    {
    //        transform.position += (Vector3.up * 0.2f) + (transform.position - hitOrigin).normalized * (pushBackDistance * 3) * Time.deltaTime;
    //        die();
    //    }

    //    //denna pushback grej måste göras bättre
    //    transform.position += (Vector3.up * 0.2f) + (transform.position - hitOrigin).normalized * pushBackDistance * Time.deltaTime;

    //    IFrameCoolDown = IFrameTime;
    //    return true;
    //}

    public void UpdateDestination(Vector3 destination, float timer)
    {
        StartCoroutine(DestinationIEnum(destination, timer));
    }

    private IEnumerator DestinationIEnum(Vector3 destination, float timer)
    {
        NavAgent.SetDestination(destination);
        yield return new WaitForSeconds(timer);
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

    //public void die()
    //{
    //    MeshRen.material.color = Color.gray;
    //    Destroy(gameObject, 1.5f);
    //}
}

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
    public State CurrectState {
        get {
            return CurrentState;
        }
    }
    public float chaseDistance;
    public float cooldown;
    //anvståndet där fienden går in i attackState
    public float attackDistance;
    public float lostTargetDistance;
    public float moveSpeed;
    public float lookRotationSpeed;
    public float hearRadius;
    public float AttackPlacmentDistance;
    public bool IsWaitAtPosition = false;
    public float waitAtPatrolPoints = 0.0f;
    public LayerMask visionMask;
    public Player player;
    private EnemyHealth healthSystem;
    [HideInInspector]public CharacterController controller;

    public float EnemyID { get { return enemyID; } }
    private float enemyID;

    private static int numberOfEnemiesInAttackState;
    public static int NumberOfEnemiesInAttackState {
        get {
            return numberOfEnemiesInAttackState;
        }
        set {
            numberOfEnemiesInAttackState = value;
        }
    }

    public AudioClip AttackClip;
    public AudioSource EnemyAudioSource { get; set; }

    protected override void Awake()
    {
        EnemyAudioSource = GetComponent<AudioSource>();
        //för debug
        MeshRen = GetComponent<MeshRenderer>();
        enemyID = transform.position.sqrMagnitude;
        GameController.GameControllerInstance.Enemies.Add(enemyID, gameObject);
        //timer = timeBetweenSetDestination;
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

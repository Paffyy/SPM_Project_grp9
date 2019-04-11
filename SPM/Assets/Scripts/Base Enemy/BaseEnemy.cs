using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : StateMachine
{
    //för debug
    [HideInInspector] public MeshRenderer MeshRen;


    public PathMaker Path;
    public int Health;
    public float IFrameTime;
    private float IFrameCoolDown;
    //[HideInInspector] public LinkedList<GameObject> BaseEnemyList = new LinkedList<GameObject>();
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public FieldOfView Fow;
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
        base.Awake();
    }

    private void Update()
    {
        IFrameCoolDown -= Time.deltaTime;
    }

    //hitOrigin normalizerad vector, används för att putta tillbaka fienden från den positionen
    public bool hit(int dmg, Vector3 hitOrigin, float pushBackDistance)
    {
        if(IFrameCoolDown > 0)
            return false;

        Health -= dmg;
        transform.position += hitOrigin * pushBackDistance * Time.deltaTime;

        IFrameCoolDown = IFrameTime;
        return true;
    }
}

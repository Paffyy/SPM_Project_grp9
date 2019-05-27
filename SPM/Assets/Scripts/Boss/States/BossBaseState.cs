using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : State
{

    //[SerializeField] protected float moveSpeed;
    //[SerializeField] protected float hearRadius;
    protected float chaseDistance;
    protected float cooldown;
    protected float attackDistance;
    protected float lostTargetDistance;
    protected float moveSpeed;
    protected float hearRadius;
    protected float PlacmentDistance;
    protected bool isWaitingAtPatrolPoints;
    protected float waitAtPatrolPointsTime;
    //public LayerMask PlayerLayer;

    private float timerSetDestination;
    private float timeBetweenSetDestination = 0.1f;
    private Vector3 currentDestination;

    //för debug
    [SerializeField] protected Material material;

    protected Boss owner;

    public override void Enter()
    {
        timerSetDestination = timeBetweenSetDestination;
        //Debug.Log("BaseState");
        owner.MeshRen.material = material;
        owner.NavAgent.speed = moveSpeed;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Boss)owner;
        chaseDistance = this.owner.chaseDistance;
        cooldown = this.owner.cooldown;
        attackDistance = this.owner.attackDistance;
        lostTargetDistance = this.owner.lostTargetDistance;
        moveSpeed = this.owner.moveSpeed;
        hearRadius = this.owner.hearRadius;
        waitAtPatrolPointsTime = this.owner.waitAtPatrolPoints;
        isWaitingAtPatrolPoints = this.owner.IsWaitAtPosition;

        //Sätter NavAgetStoppingDistance
        PlacmentDistance = this.owner.NavAgent.stoppingDistance;
    }

    public override void HandleUpdate()
    {

        timerSetDestination -= timeBetweenSetDestination;
        if (timerSetDestination < 0)
        {
            if (owner.NavAgent.enabled == true)
            {
                owner.NavAgent.SetDestination(currentDestination);
            }
            timerSetDestination = timeBetweenSetDestination;
        }

        base.HandleUpdate();

        //owner.IFrameCoolDown -= Time.deltaTime;

    }

    protected bool CanSeePlayer()
    {
        //return !Physics.Linecast(owner.transform.position, owner.player.transform.position, owner.visionMask);
        return owner.Fow.TargetsInFieldOfView() != null;
    }

    protected bool CanHearPlayer()
    {
        //kolla först om spelaren springer
        return Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearRadius;
    }

    public void UpdateDestination(Vector3 destination)
    {
         currentDestination = destination;
    }

}

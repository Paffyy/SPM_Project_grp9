using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBaseState : State
{
    protected float chaseDistance;
    protected float cooldown;
    protected float attackDistance;
    protected float lostTargetDistance;
    protected float moveSpeed;
    protected float hearRadius;
    protected float PlacmentDistance;
    protected float waitAtPatrolPoints;
    protected CharacterController controller;
    protected bool isWaitAtPosition;
    protected float lookRotationSpeed;

    private float timerSetDestination;
    private float timeBetweenSetDestination = 0.1f;
    private Vector3 currentDestination;
    private Transform currentRotationTarget;
    private GameObject firesOfHeavenContainer;

    [SerializeField] protected Material material;

    protected RangedEnemy owner;

    public override void Enter()
    {
        firesOfHeavenContainer = owner.firesOfHeavenContainer;
        timerSetDestination = timeBetweenSetDestination;
        owner.MeshRen.material = material;
        owner.NavAgent.speed = moveSpeed;
        owner.NavAgent.updateRotation = false;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (RangedEnemy)owner;
        chaseDistance = this.owner.chaseDistance;
        cooldown = this.owner.cooldown;
        attackDistance = this.owner.attackDistance;
        lostTargetDistance = this.owner.lostTargetDistance;
        moveSpeed = this.owner.moveSpeed;
        hearRadius = this.owner.hearRadius;
        waitAtPatrolPoints = this.owner.waitAtPatrolPoints;
        PlacmentDistance = this.owner.AttackPlacmentDistance;
        isWaitAtPosition = this.owner.IsWaitAtPosition;
        lookRotationSpeed = this.owner.lookRotationSpeed;
        controller = this.owner.controller;
        timerSetDestination = timeBetweenSetDestination;
    }

    public override void HandleUpdate()
    {
        timerSetDestination -= timeBetweenSetDestination;
        SetDestination();
        Rotate();
        Debug.DrawLine(owner.transform.position, currentDestination); //debuggar vart den har target
        base.HandleUpdate();
    }

    protected bool CanSeePlayer()
    {
        return owner.Fow.TargetsInFieldOfView() != null;
    }

    protected bool CanHearPlayer()
    {
        return Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearRadius;
    }

    private void SetDestination()
    {
        if (timerSetDestination < 0)
        {
            if (owner.NavAgent.enabled == true)
            {
                owner.NavAgent.SetDestination(currentDestination);
            }
            timerSetDestination = timeBetweenSetDestination;
        }
    }

    protected void UpdateDestination(Vector3 destination)
    {
        currentDestination = destination;
    }

    protected void UpdateRotation(Transform target)
    {
        currentRotationTarget = target;
    }

    private void Rotate()
    {
        if (currentRotationTarget != null)
        {
            Vector3 targetDir = currentRotationTarget.position - owner.transform.position;
            Vector3 modifiedDir = new Vector3(targetDir.x, 0, targetDir.z);

            float step = lookRotationSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(owner.transform.forward, modifiedDir, step, 0.0f);
            Debug.DrawRay(owner.transform.position, newDir, Color.red);

            owner.transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

}

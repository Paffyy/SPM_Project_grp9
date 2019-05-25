using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBaseState : State
{
    protected CharacterController controller;
    protected RangedBaseEnemy owner;
    protected float lookRotationSpeed;
    protected float cooldown;
    protected float attackDistance;
    protected float lostTargetDistance;
    protected float moveSpeed;
    protected float hearRadius;


    [SerializeField] protected Material material;

    private Transform currentRotationTarget;
    private Vector3 currentDestination;
    private float timerSetDestination;
    private float timeBetweenSetDestination = 0.1f;

    public override void Enter()
    {
        timerSetDestination = timeBetweenSetDestination;
        owner.MeshRen.material = material;
        owner.NavAgent.speed = moveSpeed;
        owner.NavAgent.updateRotation = false;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (RangedBaseEnemy)owner;
        timerSetDestination = timeBetweenSetDestination;
        cooldown = this.owner.cooldown;
        attackDistance = this.owner.attackDistance;
        lostTargetDistance = this.owner.lostTargetDistance;
        controller = this.owner.controller;
        hearRadius = this.owner.hearRadius;
        lookRotationSpeed = this.owner.lookRotationSpeed;
        lostTargetDistance = this.owner.lostTargetDistance;
        moveSpeed = this.owner.moveSpeed;
    }

    public override void HandleUpdate()
    {
        timerSetDestination -= timeBetweenSetDestination;
        Rotate();
        base.HandleUpdate();
    }

    protected void SetDestination()
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

    protected void UpdateRotation(Transform target)
    {
        currentRotationTarget = target;
    }

    protected void UpdateDestination(Vector3 destination)
    {
        currentDestination = destination;
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

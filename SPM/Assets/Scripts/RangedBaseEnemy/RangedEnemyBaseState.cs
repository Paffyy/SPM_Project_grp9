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

    [SerializeField] protected Material material;

    private Transform currentRotationTarget;

    public override void Enter()
    {
        owner.NavAgent.updateRotation = false;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (RangedBaseEnemy)owner;
        cooldown = this.owner.cooldown;
        attackDistance = this.owner.attackDistance;
        lostTargetDistance = this.owner.lostTargetDistance;
        controller = this.owner.controller;
    }

    public override void HandleUpdate()
    {
        Rotate();
        base.HandleUpdate();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBaseState : State
{

    //[SerializeField] protected float moveSpeed;
    //[SerializeField] protected float hearRadius;
    protected float chaseDistance;
    protected float cooldown;
    protected float attackDistance;
    protected float lostTargetDistance;
    protected float moveSpeed;
    protected float hearRadius;
    //public LayerMask PlayerLayer;

    //för debug
    [SerializeField] protected Material material;

    protected BaseEnemy owner;

    public override void Enter()
    {
        Debug.Log("BaseState");
        owner.MeshRen.material = material;
        owner.NavAgent.speed = moveSpeed;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (BaseEnemy)owner;
        chaseDistance = this.owner.chaseDistance;
        cooldown = this.owner.cooldown;
        attackDistance = this.owner.attackDistance;
        lostTargetDistance = this.owner.lostTargetDistance;
        moveSpeed = this.owner.moveSpeed;
        hearRadius = this.owner.hearRadius;
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        //debug shit
        if (Input.GetKeyDown("h"))
        {
            owner.hit(2, owner.player.transform.position, 20.0f);
        }
        //debug shit

        owner.IFrameCoolDown -= Time.deltaTime;
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

}

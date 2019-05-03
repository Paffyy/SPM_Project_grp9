using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/AttackState")]
public class BossAttackState : BaseEnemyBaseState
{

    //[SerializeField] private float chaseDistance;
    //[SerializeField] private float cooldown;
    //public float PlacmentDistance;

    [Range(0, 1)] public float HealthPhase1;
    [Range(0, 1)] public float HealthPhase2;
    [Range(0, 1)] public float HealthPhase3;
    public float TimePhase1;
    public float TimePhase2;
    public float TimePhase3;

    private float ShockWaveAttackDistance = 10.5f;

    private float timeSinceLastSpecialAttack;
    private float specialTimer = 5.0f;

    private float timer = 0.0f;

    private float currentCooldown;
    private bool backOff = false;
    private bool toPlayer = false;
    private bool circle = false;
    private float backTimer = 2.0f;

    private float circleDistance = 1.5f;

    public override void Enter()
    {
        base.Enter();
        //owner.MeshRen.material.color = Color.red;
        currentCooldown = cooldown;
        owner.currectState = this;
        timer = backTimer;
        timeSinceLastSpecialAttack = specialTimer;
        //hitta hur många andra fiender som är i AttackState
    }

    public override void HandleUpdate()
    {
        timeSinceLastSpecialAttack -= Time.deltaTime;
        Debug.Log(timeSinceLastSpecialAttack);
        owner.UpdateDestination(owner.player.transform.position, 0.1f);

        //tittar på spelaren
        LookAtTarget(owner.player.transform);


        //Vanlig attack
        if(Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance && owner.Fow.TargetsInFieldOfView() != null)
        {
            Attack();
        }


        //Forward Shockwave attack
        if(/*Mathf.Clamp(owner.healthSystem.CurrentHealth, 0, owner.healthSystem.StartingHealth) > HealthPhase1 &&*/
            Vector3.Distance(owner.transform.position, owner.player.transform.position) > ShockWaveAttackDistance && timeSinceLastSpecialAttack < 0)
        {
            owner.Transition<BossForwardShockwaveState>();
        }

        base.HandleUpdate();
    }

    void LookAtTarget(Transform target)
    {
        Vector3 dir = (target.position - owner.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, lookRotation, 5f);
    }

    //void BackOff()
    //{
    //    owner.NavAgent.updateRotation = false;
    //    //owner.transform.LookAt(owner.player.transform);
    //    owner.UpdateDestination(-owner.transform.forward * 10, 2f);
    //    //owner.NavAgent.updateRotation = true;
    //    //Debug.Log("back off");
    //}



    //void CirclePlayer()
    //{
    //    float angleDiviation = 1.5f;
    //    float currentDistance = Vector3.Distance(owner.transform.position, owner.player.transform.position);
    //    float currentAngle = Vector3.Angle(owner.player.transform.position, owner.transform.position);
    //    float randomAgle = Random.Range(-angleDiviation, angleDiviation);

    //    Vector3 position = owner.player.transform.forward * currentDistance;

    //    float x = owner.player.transform.position.x + currentDistance * Mathf.Cos(currentAngle + randomAgle);
    //    float y = owner.player.transform.position.y + currentDistance * Mathf.Sin(currentAngle + randomAgle);

    //    owner.UpdateDestination(new Vector3(x, y, owner.transform.position.z), 0.1f);
    //}

    //private void JumpAttack()
    //{
    //    owner.controller.MovePosition(Vector3.up * jumpHeight);
    //    while (owner.controller.IsGrounded() == false)
    //    {

    //    }

    //    currentJumpTimer = jumpAttackTimer;
    //}

    private void Attack()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0)
            return;

        //Skadar spelarn
        GameObject[] arr = owner.Fow.TargetsInFieldOfView();
            for (int i = 0; i < arr.Length; i++)
            {
                //Debug.Log(arr[i]);
                arr[i].GetComponent<PlayerHealth>().TakeDamage(owner.Damage);
            }
        currentCooldown = cooldown;
    }

}

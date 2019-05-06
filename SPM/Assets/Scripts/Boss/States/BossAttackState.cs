﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/AttackState")]
public class BossAttackState : BossBaseState
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
    private float timeSinceShockwave;
    public float ShockWaveTimer;

    private float FiresOfHeavenDistance = 5.0f;
    private float timeSinceFiresOfHeaven;
    public float FiresOfHeavenTimer;

    private float currentCooldown;

    public override void Enter()
    {
        base.Enter();
        //owner.MeshRen.material.color = Color.red;
        currentCooldown = cooldown;
        owner.currectState = this;
        timeSinceShockwave = ShockWaveTimer;
        timeSinceFiresOfHeaven = FiresOfHeavenTimer;
        //hitta hur många andra fiender som är i AttackState
    }

    public override void HandleUpdate()
    {
        timeSinceShockwave -= Time.deltaTime;
        owner.UpdateDestination(owner.player.transform.position, 0.5f);

        //tittar på spelaren
        LookAtTarget(owner.player.transform);


        //Vanlig attack
        if(Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance && owner.Fow.TargetsInFieldOfView() != null)
        {
            Attack();
        }

        //debug shit
        if (Input.GetKey(KeyCode.E))
        {
            owner.Transition<BossFiresOfHeavenState>();
        }
        //debug shit

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > ShockWaveAttackDistance && timeSinceShockwave < 0)
        {

            int rand = Random.Range(1,3);
            switch(rand)
            {
                case 1:
                case 2:
                    owner.Transition<BossForwardShockwaveState>();
                    break;
                //case 1:
                //case 2:
                case 3:
                    owner.Transition<BossFiresOfHeavenState>();
                    break;
            }
        }

        ////Forward Shockwave attack
        //if (/*Mathf.Clamp(owner.healthSystem.CurrentHealth, 0, owner.healthSystem.StartingHealth) > HealthPhase1 &&*/
        //    Vector3.Distance(owner.transform.position, owner.player.transform.position) > ShockWaveAttackDistance && timeSinceShockwave < 0)
        //{
        //    owner.Transition<BossForwardShockwaveState>();
        //}

        ////Fires of Heaven attack
        //if(Vector3.Distance(owner.transform.position, owner.player.transform.position) > FiresOfHeavenDistance && timeSinceFiresOfHeaven < 0)
        //{
        //    owner.Transition<BossFiresOfHeavenState>();
        //}

        base.HandleUpdate();
    }

    void LookAtTarget(Transform target)
    {
        Vector3 dir = (target.position - owner.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, lookRotation, 5f);
    }


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
            PlayerHealth health = arr[i].GetComponent<PlayerHealth>();
            Vector3 push = (((health.transform.position) - owner.transform.position).normalized + Vector3.up * 2) * owner.PushBack;
            health.TakeDamage(owner.Damage, push, owner.transform.position);
            }
        currentCooldown = cooldown;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/AttackState")]
public class BossAttackState : BossBaseState
{

    //procet av hälsan
    [Range(0, 1)] public float HealthPhase1;
    //[Range(0, 1)] public float HealthPhase2;
    //[Range(0, 1)] public float HealthPhase3;

    public float TimePhase1;
    //public float TimePhase2;
    //public float TimePhase3;

    private float ShockWaveAttackDistance = 10.5f;
    private float timeSinceShockwave;
    public float ShockWaveTimer;

    private float FiresOfHeavenDistance = 5.0f;
    private float timeSinceFiresOfHeaven;
    public float FiresOfHeavenTimer;

    private float currentCooldown;
    private bool doubleAttackAllowed = false;

    public override void Enter()
    {
        base.Enter();
        //owner.MeshRen.material.color = Color.red;
        currentCooldown = cooldown;
        timeSinceShockwave = ShockWaveTimer;
        timeSinceFiresOfHeaven = FiresOfHeavenTimer;
        //hitta hur många andra fiender som är i AttackState
    }

    public override void HandleUpdate()
    {
        timeSinceShockwave -= Time.deltaTime;
        UpdateDestination(owner.player.transform.position);

        //tittar på spelaren
        LookAtTarget(owner.player.transform);


        //Vanlig attack
        if(Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance && owner.Fow.TargetsInFieldOfView() != null)
        {
            if (doubleAttackAllowed)
                DoubleAttack();
            SingelAttack();
        }

        //debug shit
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    owner.Transition<BossFiresOfHeavenState>();
        //}
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    owner.Transition<BossForwardShockwaveState>();
        //}
        //debug shit

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > ShockWaveAttackDistance && timeSinceShockwave < 0)
        {

            int rand = Random.Range(1,4);
            Debug.Log("Random " + rand);
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


    private void SingelAttack()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0)
            return;

        //Skadar spelarn
        owner.anim.SetTrigger("AttackRight");
        //GameObject[] arr = owner.Fow.TargetsInFieldOfView();
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //    //Debug.Log(arr[i]);
        //    PlayerHealth health = arr[i].GetComponent<PlayerHealth>();
        //    Vector3 push = (((health.transform.position) - owner.transform.position).normalized + Vector3.up * 2) * owner.PushBack;
        //    health.TakeDamage(owner.Damage, push, owner.transform.position);
        //    }

        doubleAttackAllowed = true;
        currentCooldown = cooldown;
    }

    private void DoubleAttack()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0)
            return;

        //Skadar spelarn
        owner.anim.SetTrigger("AttackRight");

        owner.anim.SetTrigger("AttackLeft");
        //GameObject[] arr = owner.Fow.TargetsInFieldOfView();
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //    //Debug.Log(arr[i]);
        //    PlayerHealth health = arr[i].GetComponent<PlayerHealth>();
        //    Vector3 push = (((health.transform.position) - owner.transform.position).normalized + Vector3.up * 2) * owner.PushBack;
        //    health.TakeDamage(owner.Damage, push, owner.transform.position);
        //    }

        doubleAttackAllowed = false;
        //En liten extra coolDown för dubbelattacken
        currentCooldown = cooldown + 0.2f;
    }

}

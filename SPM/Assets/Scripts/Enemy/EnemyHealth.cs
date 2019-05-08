using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    //public float DamageCooldown;
    //private float currentCooldown;
    private float StunTimer;
    private float currenTimer;
    public Slider EnemyHealthSlider;
    private CharacterController controller;
    private NavMeshAgent navAgent;
    // Start is called before the first frame update
    public virtual void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        SetupHealthSlider();
        DmgCoolDownTimer = StunTimer;
    }

    public void SetupHealthSlider()
    {
        CurrentHealth = StartingHealth;
        EnemyHealthSlider.maxValue = StartingHealth;
        //currentCooldown = DamageCooldown;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //currentCooldown -= Time.deltaTime;
    //}

    public override void Update()
    {
        base.Update();
        currenTimer -= Time.deltaTime;
        if(navAgent != null && controller != null)
        {
            if (currenTimer < 0 && navAgent.isStopped == true)
            {
                navAgent.isStopped = false;
                controller.enabled = false;
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        if (!CanTakeDamage())
            return;
        else
            RestartCoolDown();
        CurrentHealth -= damage;
        EnemyHealthSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
            EnemyDead();
    }

    public override void TakeDamage(int damage, Vector3 pushBack, Vector3 position)
    {
        if (!CanTakeDamage())
            return;
        else
            RestartCoolDown();
        CurrentHealth -= damage;
        EnemyHealthSlider.value = CurrentHealth;
        if(navAgent != null && controller != null)
        {
            navAgent.isStopped = true;

            //controller.Velocity += pushBack;
            controller.enabled = true;
            controller.MovePosition(pushBack);
        }
        if (CurrentHealth <= 0)
            EnemyDead();
    }

    public void EnemyDead()
    {
        EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, new DeathEventInfo(gameObject));
        Destroy(this.gameObject);
    }
}

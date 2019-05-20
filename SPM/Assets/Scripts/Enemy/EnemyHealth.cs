using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    //public float DamageCooldown;
    //private float currentCooldown;
    private float navMeshAgentOffTime;
    public Slider EnemyHealthSlider;
    private CharacterController controller;
    private NavMeshAgent navAgent;

    private bool isDead = false;

    private BaseEnemy thisEnemy;
    // Start is called before the first frame update
    public virtual void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        thisEnemy = GetComponent<BaseEnemy>();
        SetupHealthSlider();
        EnemyHealthSlider.value = EnemyHealthSlider.maxValue;
    }

    public void SetupHealthSlider()
    {
        CurrentHealth = StartingHealth;
        EnemyHealthSlider.maxValue = StartingHealth;
    }

    public override void Update()
    {
        base.Update();
        navMeshAgentOffTime += Time.deltaTime;
        if(navAgent != null && controller != null)
        {
            //måste vänta en stund för att kolla om isGrounded för annars kommer inte fienden upp från marken
            if (navAgent.enabled == false && navMeshAgentOffTime > 0.1f && controller.IsGrounded())
            {
                navAgent.enabled = true;
                controller.enabled = false;
            }
        }

        if (CurrentHealth <= 0 && isDead == false)
        {
            isDead = true;
            EnemyDead();
        }
    }

    public override void TakeDamage(int damage, bool overrideCooldown = false)
    {
        if (!overrideCooldown)
        {
            if (!CanTakeDamage())
                return;
            else
                RestartCoolDown();
        }
        CurrentHealth -= damage;
        EnemyHealthSlider.value = CurrentHealth;

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
            TakeDamage(damage);
            Debug.Log("Fiende \"" + gameObject.name + "\" hade ingen controller men någon försökte göra pushback på den");
            navAgent.enabled = false;
            controller.enabled = true;
            //controller.MovePosition(pushBack);
            controller.Velocity += pushBack;
        }
            //navAgent.enabled = false;
            //controller.enabled = true;
            ////controller.MovePosition(pushBack);
            //controller.Velocity += pushBack;
            navMeshAgentOffTime = 0.0f;
    }

    private void ToAttackState()
    {
        if(thisEnemy != null)
        {
            thisEnemy.Transition<BaseEnemyAttackState>();
        }
    }

    public void EnemyDead()
    {
        EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, new DeathEventInfo(gameObject));
        GameControl.GameController.DeadEnemies.Add(gameObject.GetComponent<Enemy>().EnemyID);
        if (thisEnemy != null)
        {
            thisEnemy.Transition<BaseEnemyDeathState>();
        }
        //Destroy(this.gameObject);
    }
}

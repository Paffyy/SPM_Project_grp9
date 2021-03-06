﻿using System.Collections;
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
    [SerializeField]
    private GameObject enemyCanvas;
    private bool isHealthBarVisible;

    private BaseEnemy thisEnemy;

    public virtual void Start()
    {
        isHealthBarVisible = false;
      //  enemyCanvas.SetActive(false);
        navAgent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        thisEnemy = GetComponent<BaseEnemy>();
        CurrentHealth = StartingHealth;
    }

    public void SetupHealthSlider()
    {
       // CurrentHealth = StartingHealth;
        EnemyHealthSlider.maxValue = StartingHealth;
        EnemyHealthSlider.value = EnemyHealthSlider.maxValue;
    }

    public override void Update()
    {
        base.Update();
        if (navAgent != null && controller != null)
        {
            //måste vänta en stund för att kolla om isGrounded för annars kommer inte fienden upp från marken
            if (isDead == false && navAgent.enabled == false && navMeshAgentOffTime > 0.1f && controller.IsGrounded())
            {
                navAgent.enabled = true;
                controller.enabled = false;
            }
            else if (isDead == true && navMeshAgentOffTime > 0.1f && controller.IsGrounded())
            {
                controller.enabled = true;
            }
        }
        if (!navAgent.enabled)
        {
            navMeshAgentOffTime += Time.deltaTime;
        }
        if (CurrentHealth <= 0 && isDead == false)
        {
            isDead = true;
            EnemyDead();
        }
    }

    public override void TakeDamage(int damage, bool overrideCooldown = false)
    {
        if (isHealthBarVisible == false)
        {
            SetupHealthSlider();
            ActivateHealthBar();
            isHealthBarVisible = true;
        }
        if (!overrideCooldown)
        {
            if (!CanTakeDamage())
                return;
            else
                RestartCoolDown();
        }
        CurrentHealth -= damage;
        EnemyHealthSlider.value = CurrentHealth;
        SpawnHeartParticles();

    }

    public void ActivateHealthBar()
    {
        //enemyCanvas.SetActive(true);
        //enemyCanvas.GetComponent<Canvas>().enabled = true;
        //isHealthBarVisible = true;
    }

    public override void TakeDamage(int damage, Vector3 pushBack, Vector3 position)
    {
        if (isHealthBarVisible == false)
        {
            SetupHealthSlider();
            ActivateHealthBar();
            isHealthBarVisible = true;
        }
        if (!CanTakeDamage())
            return;
        else
            RestartCoolDown();
        CurrentHealth -= damage;
        EnemyHealthSlider.value = CurrentHealth;
        SpawnHeartParticles();
        if (navAgent != null && controller != null)
        {
            TakeDamage(damage);
            navAgent.enabled = false;
            controller.enabled = true;
            navMeshAgentOffTime = 0.0f;
            controller.Velocity = Vector3.zero;
            controller.Velocity += pushBack;
        }
    }

    private void SpawnHeartParticles()
    {
        EventHandler.Instance.FireEvent(EventHandler.EventType.ParticleEvent, 
            new ParticleSpawnEventInfo(GetComponent<CapsuleCollider>().ClosestPoint(Vector3.up * 1000), ParticleSpawnEventInfo.Particle.Hearts));
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
        EventHandler.Instance.FireEvent(EventHandler.EventType.AudioEvent, new AudioEventInfo(thisEnemy.DeathSound));
        EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, new DeathEventInfo(gameObject));
        GameController.GameControllerInstance.DeadEnemies.Add(gameObject.GetComponent<BaseEnemy>().EnemyID);
        if(thisEnemy != null)
        {
            if(GetComponent<RangedBaseEnemy>() != null)
            {
                thisEnemy.Transition<RangedEnemyDeathState>();
                return;
            }
            thisEnemy.Transition<BaseEnemyDeathState>();
        } else 
        {
            Destroy(gameObject, 0.5f);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 100;
    public int CurrentHealth;
    public Slider HealthSlider;
    public GameObject ShieldObject;
    private Player player;
    private Shield shield;
    public float DamageCooldown;
    private float currentCooldown;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponent<Player>();
        shield = player.GetComponent<Weapon>().Shield.GetComponent<Shield>();
    }
    void Start()
    {
        CurrentHealth = StartingHealth;
        if (HealthSlider != null)
        {
            HealthSlider.maxValue = StartingHealth;
        }
        currentCooldown = DamageCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (currentCooldown > 0)
            return;
        else
            currentCooldown = DamageCooldown;

        CurrentHealth -= damage;
        HealthSlider.value = CurrentHealth;

        if (CurrentHealth <= 0)
            PlayerDead();
    }

    //ger spelaren skada med pushback
    //om spelaren inte ska kunna ta damage, skicka in Vector3.zero på position
    public void TakeDamage(int damage, Vector3 pushBack, Vector3 position)
    {
        if (currentCooldown > 0)
            return;
        else
            currentCooldown = DamageCooldown;

        //TODO fixa så att skölden fungerar på riktigt
        //Okej den funkar fortfarande inte pallar inte fixa det just nu, det borde vara så att skölden har ett state där den blockar allt
        //eller om man gör så skickar man bara ut en ray mellan fienden och spelaren och kollar om skölden kommer emellan
        //men det kommer nog göra det svårt för animerade attacker så det är nog bättre med någon form av dot produkt

        //var shield = GetComponentInChildren<Shield>();
        if (ShieldObject != null)
        {
            var dotProduct = Vector3.Dot(ShieldObject.transform.TransformDirection(transform.forward), position - transform.position);
            if (dotProduct > ShieldObject.transform.forward.x && shield.IsBlocking) // shieldblocked
            {
                //ShieldObject.TakeDamage(damage);
                //skölden tar bort 90% av pushBack effekten
                player.Velocity += pushBack * 0.4f;
                Debug.Log("shield hit!");
                return;
            }
        }

        CurrentHealth -= damage;
        HealthSlider.value = CurrentHealth;
        player.Velocity += pushBack;

        if (CurrentHealth <= 0)
            PlayerDead();
    }
    
    public void AddHealth(int health)
    {
        if(CurrentHealth < 100)
        {
            CurrentHealth += health;
            if(CurrentHealth > 100)
            {
                CurrentHealth = 100;
            }
            HealthSlider.value = CurrentHealth;
        }
    }

    public void PlayerDead()
    {
        DeathEventInfo deathEventInfo = new DeathEventInfo(gameObject);
        EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, deathEventInfo);
        CurrentHealth = 100;
        HealthSlider.value = CurrentHealth;
        //Destroy(this.gameObject);
    }
}

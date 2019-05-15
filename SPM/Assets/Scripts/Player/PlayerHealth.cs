using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 100;
    public int CurrentHealth;
    public Slider HealthSlider;
    private Player player;

    public float DamageCooldown;
    private float currentCooldown;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = StartingHealth;
        if (HealthSlider != null)
        {
            HealthSlider.maxValue = StartingHealth;
        }
        currentCooldown = DamageCooldown;
        player = GetComponent<Player>();
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

        var shield = GetComponentInChildren<Shield>();
        if (shield != null)
        {
            var dotProduct = Vector3.Dot(shield.transform.TransformDirection(transform.forward), position - transform.position);
            if (dotProduct > shield.FacingOffset) // shieldblocked
            {
                shield.TakeDamage(damage);
                //skölden tar bort 50% av pushBack effekten
                player.Velocity += pushBack * 0.5f;
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

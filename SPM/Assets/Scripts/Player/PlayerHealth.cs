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
    public void TakeDamage(int damage, Vector3 pushBack)
    {
        if (currentCooldown > 0)
            return;
        else
            currentCooldown = DamageCooldown;

        CurrentHealth -= damage;
        HealthSlider.value = CurrentHealth;

        player.Velocity += pushBack + Vector3.up;

        if (CurrentHealth <= 0)
            PlayerDead();
    }

    public void PlayerDead()
    {
        DeathEventInfo deathEventInfo = new DeathEventInfo(Player);
        EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, deathEventInfo);
        CurrentHealth = 100;
        HealthSlider.value = CurrentHealth;
        //Destroy(this.gameObject);
    }
}

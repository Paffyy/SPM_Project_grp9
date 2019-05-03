using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject Player;
    public int StartingHealth = 100;
    public int CurrentHealth;
    public Slider HealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = StartingHealth;
        if (HealthSlider != null)
        {
            HealthSlider.maxValue = StartingHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
            PlayerDead();
    }

    public void PlayerDead()
    {
        DeathEventInfo deathEventInfo = new DeathEventInfo(Player);
        EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, deathEventInfo);
        CurrentHealth = 100;
        HealthSlider.value = CurrentHealth;
        //Destroy(Player);
    }
}

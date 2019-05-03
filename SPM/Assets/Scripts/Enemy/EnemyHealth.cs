using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject Enemy;
    public int StartingHealth = 100;
    public int CurrentHealth;
    public float DamageCooldown;
    private float currentCooldown;
    public Slider EnemyHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        SetupHealthSlider();
    }

    public void SetupHealthSlider()
    {
        CurrentHealth = StartingHealth;
        EnemyHealthSlider.maxValue = StartingHealth;
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
        EnemyHealthSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
            EnemyDead();
    }

    public void EnemyDead()
    {
        Destroy(Enemy);
    }
}

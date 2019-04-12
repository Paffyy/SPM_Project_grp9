using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject Enemy;
    public int StartingHealth = 100;
    public int CurrentHealth;
    public Slider EnemyHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = StartingHealth;
        EnemyHealthSlider.maxValue = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
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

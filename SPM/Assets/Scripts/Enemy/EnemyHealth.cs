using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject Enemy;
    public int StartingHealth = 100;
    public int CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0)
            EnemyDead();
    }

    public void EnemyDead()
    {
        Debug.Log("EnemyDead");
        //Destroy(Enemy);
    }
}

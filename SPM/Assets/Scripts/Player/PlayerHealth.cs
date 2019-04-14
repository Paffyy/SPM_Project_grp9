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
        HealthSlider.maxValue = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthSlider.value = CurrentHealth;
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0)
            PlayerDead();
    }

    public void PlayerDead()
    {
        Player.transform.position = Manager.Instance.GetCheckPoint();
        Debug.Log("PlayerDead");
        //Destroy(Player);
    }
}

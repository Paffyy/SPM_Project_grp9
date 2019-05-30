using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HeartsHandeler : MonoBehaviour
{
    [SerializeField] private int totalNumberOfHearts;
    [SerializeField] private int currentNumberOfHearts;
    //public List<Heart> heartObjects = new List<Heart>();
    public Heart[] heartObjects;

    private int healthOfHeart = 10;
    //private int maxHealth = 100;

    public int CurrentHealth { private get { return currentHealth; } set { currentHealth = value; CheckHealth(); } }
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        SetupHealth();
    }


    void SetupHealth()
    {
        heartObjects = GetComponentsInChildren<Heart>();
        totalNumberOfHearts = heartObjects.Length;
        CheckHealth();
    }

    void CheckHealth()
    {
        int damage = CurrentHealth / totalNumberOfHearts;

        bool halfLife = (CurrentHealth % totalNumberOfHearts == 0) ? false : true;

        for (int i = 0; i < heartObjects.Length; i++)
        {
            if(i < damage)
            {
                heartObjects[i].SetHeart(Heart.HeartState.Full);
            }
            else if(i == damage && halfLife == true)
            {
                heartObjects[i].SetHeart(Heart.HeartState.Half);
            }
            else
            {
                heartObjects[i].SetHeart(Heart.HeartState.Empty);
            }
            
        }
    }
}

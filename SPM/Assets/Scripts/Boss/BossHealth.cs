using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{

    Boss boss;
    public override void Start()
    {
        base.Start();
        boss = GetComponent<Boss>();

    }

    public override void TakeDamage(int damage, bool overrideCooldown = false)
    {
        if (!overrideCooldown)
        {
            if (!CanTakeDamage())
                return;
        }
        RestartCoolDown();
        CurrentHealth -= damage;
        EnemyHealthSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
            EnemyDead();
    }

    public override void TakeDamage(int damage, Vector3 pushBack, Vector3 position)
    {
        base.TakeDamage(damage, Vector3.zero, position); // ingen pushback
    }

    //override public bool CanTakeDamage()
    //{
    //    //bossen kan inte ta skada i BossFiresOfHeavenState
    //    return base.CanTakeDamage();
    //}


}

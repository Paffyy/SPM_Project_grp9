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

    public override void TakeDamage(int damage)
    {

        if (!CanTakeDamage())
            return;
        Debug.Log("boop");
        RestartCoolDown();
        CurrentHealth -= damage;
        EnemyHealthSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
            EnemyDead();
    }

    override public bool CanTakeDamage()
    {
        //bossen kan inte ta skada i BossFiresOfHeavenState
        return base.CanTakeDamage() && boss.currectState.GetType() != typeof(BossFiresOfHeavenState);
    }


}

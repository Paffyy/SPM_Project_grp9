using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int StartingHealth = 100;
    public int CurrentHealth;
    public float DmgCoolDownTimer;
    private float timer;

    public abstract void TakeDamage(int damage, bool overrideCooldown);
    public virtual void TakeDamage(int damage, Vector3 pushBack, Vector3 position) { }


    // Start is called before the first frame update
    void Start()
    {
        timer = DmgCoolDownTimer;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        timer -= Time.deltaTime;

    }

    virtual public bool CanTakeDamage()
    {
        return timer < 0;
    }

    public void RestartCoolDown()
    {
        timer = DmgCoolDownTimer;
    }

    public void RestartCoolDown(float dmgCoolDownTime)
    {
        timer = dmgCoolDownTime;
    }
}

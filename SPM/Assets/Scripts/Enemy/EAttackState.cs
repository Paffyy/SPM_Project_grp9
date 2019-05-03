using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EAttackState")]
public class EAttackState : EnemyBaseState
{
    public float delay = 2f;
    private float delayCounter;

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        Vector3 direction = owner.Player.transform.position - owner.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        owner.transform.rotation = rotation;
        delayCounter -= Time.deltaTime;
        if (delayCounter <= 0){
             Shoot();
            delayCounter = delay;
        }
       // Debug.Log("Attack");
        if (!IsAggroed())
        {
            owner.Transition<IdleState>();
        }
    }

    public void Shoot()
    {
        var clone = Instantiate(owner.Projectile, owner.transform.position, owner.transform.rotation);
        clone.GetComponent<Projectile>().Velocity = clone.GetComponent<Projectile>().Speed * (owner.Player.transform.position - clone.transform.position).normalized;
    }
}

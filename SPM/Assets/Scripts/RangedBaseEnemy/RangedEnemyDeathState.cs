using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RangedBaseEnemy/DeathSate")]
public class RangedEnemyDeathState : RangedEnemyBaseState
{
    private float timeToDespawn = 3.0f;
    private float timer;
    public override void Enter()
    {
        owner.Anim.enabled = false;
        owner.NavAgent.enabled = false;
        owner.controller.enabled = true;
        owner.controller.Velocity = Vector3.up * 15;
        owner.GetComponent<Collider>().enabled = false;
        timer = timeToDespawn;

        owner.transform.rotation = new Quaternion(180, 0, 0, 0);

        owner.controller.CollisionMask = LayerMask.NameToLayer("Enviroment");
        base.Enter();
    }

    public override void HandleUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(owner.gameObject);
        }
        base.HandleUpdate();
    }
}

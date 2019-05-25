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
        owner.NavAgent.enabled = false;
        owner.controller.enabled = true;
        owner.GetComponent<Collider>().enabled = false;
        timer = timeToDespawn;

        //bara för test ska göras med en animation sen
        owner.transform.rotation = new Quaternion(180, 0, 0, 0);

        owner.controller.CollisionMask = LayerMask.NameToLayer("Enviroment");
        base.Enter();
    }

    public override void HandleUpdate()
    {
        //owner.NavAgent.enabled = false;
        //owner.controller.enabled = true;
        //Debug.Log("navagent edabled " + owner.NavAgent.enabled + "\n controller enabled " + owner.controller.enabled);

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(owner.gameObject);
        }
        base.HandleUpdate();
    }
}

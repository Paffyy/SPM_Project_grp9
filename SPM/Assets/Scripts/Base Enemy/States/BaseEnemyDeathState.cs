using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/DeathSate")]
public class BaseEnemyDeathState : BaseEnemyBaseState
{
    private float timeToDespawn  = 3.0f;
    private float timer;
    public override void Enter()
    {
        Debug.Log("Dead");
        owner.NavAgent.enabled = false;
        owner.controller.enabled = true;
        owner.GetComponent<Collider>().enabled = false;
        owner.transform.rotation = new Quaternion(0,0, 0, 90);
        timer = timeToDespawn;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        owner.NavAgent.enabled = false;
        owner.controller.enabled = true;
        //Debug.Log("navagent edabled " + owner.NavAgent.enabled + "\n controller enabled " + owner.controller.enabled);

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Destroy(owner.gameObject);
        }
        base.HandleUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/ForwardShockwaveState")]
public class BossForwardShockwaveState : BossBaseState
{
    public float YOfset;
    //public GameObject ShockWaveObject;

    public override void Enter()
    {
        base.Enter();
        owner.anim.SetTrigger("ShockWave");
        owner.NavAgent.isStopped = true;

    }

    public override void HandleUpdate()
    {
        


        base.HandleUpdate();

        owner.Transition<BossAttackState>();


    }

    public override void Exit()
    {
        owner.NavAgent.isStopped = false;
        base.Exit();
    }

    //Är tvungen att göras i Boss scriptet så att animationen kan nå den
    //public void SpawnShockWave()
    //{
    //    Debug.Log("spawn");
    //    //Y-värdet är beroende på offseten på showwave
    //    GameObject.Instantiate(ShockWaveObject, new Vector3(owner.transform.position.x, YOfset, owner.transform.position.z),
    //    owner.transform.rotation);
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ranged/FiresOfHeavenState")]
public class FiresOfHeavenState : RangedBaseState
{
    public int NumberOfFires;
    private BoxCollider fireArea;
    public float StartingHeight;
    public GameObject FireBall;

    //tid mellan eldbollar
    public float BetweenTime = 0.5f;

    private Vector3[] vecArr;
    private int count = 0;
    private float coolDown;

    float minX;
    float maxX;
    float maxZ;
    float minZ;

    public override void Enter()
    {
        
        owner.anim.SetBool("FireRain", true);
        owner.NavAgent.isStopped = true;
        base.Enter();

    }
    public override void HandleUpdate()
    {
        cooldown -= Time.deltaTime;
        if(cooldown < 0)
        {
            SpawnFire();
            cooldown = BetweenTime;
        }

        base.HandleUpdate();
            
    }

    private void SpawnFire()
    {
        if (count < NumberOfFires)
        {
            Vector3 vec = SpawnNearPlayer(2.0f);
            GameObject obj = GameObject.Instantiate(FireBall, vec, Quaternion.identity, owner.transform);
            Destroy(obj, 10f);
            count++;
        }
        else
        {
            owner.Transition<RangedChaseState>();
        }
    }

    public Vector3 SpawnNearPlayer(float distance)
    {
        float max = distance;
        float min = -distance;
        return new Vector3(Random.Range(min, max) + owner.player.transform.position.x, owner.player.transform.position.y + StartingHeight, Random.Range(min, max) + owner.player.transform.position.z);
    }

    public override void Exit()
    {
        owner.anim.SetBool("FireRain", false);
        owner.NavAgent.isStopped = false;
        base.Exit();
    }
}

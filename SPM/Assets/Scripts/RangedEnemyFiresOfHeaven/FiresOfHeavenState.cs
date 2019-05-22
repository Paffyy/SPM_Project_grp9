using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ranged/FiresOfHeavenState")]
public class FiresOfHeavenState : RangedBaseState
{
    public int NumberOfFires;
    private BoxCollider fireArea;
    public float StartingHeight;
    public GameObject FireBallObject;

    //tid mellan eldbollar
    public float BetweenTime = 1f;

    private Vector3[] vecArr;
    private int count;
    private float coolDown;
    private bool isTransitioning;
    float minX;
    float maxX;
    float maxZ;
    float minZ;
    float spawnCooldown = 2;
    private bool isRecharging;
   
    public override void Enter()
    {
        FireBallObject.GetComponent<FireBall>().parent = owner.firesOfHeavenContainer.transform;
        owner.anim.SetBool("FireRain", true);
        owner.NavAgent.isStopped = true;
        count = 0;
        spawnCooldown = 2;
        isRecharging = false;
        base.Enter();

    }
    public override void HandleUpdate()
    {
        UpdateRotation(owner.player.transform);
        cooldown -= Time.deltaTime;
        if(cooldown < 0)
        {
            SpawnFire();
            cooldown = BetweenTime;
        }
        if (isRecharging)
        {
            spawnCooldown -= Time.deltaTime;
        }
        base.HandleUpdate();
            
    }

    private void SpawnFire()
    {
        if (count < NumberOfFires)
        {
            Vector3 vec = SpawnNearPlayer(2.0f);
            GameObject obj = Instantiate(FireBallObject, vec, Quaternion.identity, owner.firesOfHeavenContainer.transform);

            Destroy(obj, 8f);
            count++;
        }
        else
        {
            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > owner.lostTargetDistance)
            {
                owner.Transition<RangedEnemyPatrolState>();
            }

            isRecharging = true;
            if (spawnCooldown < 0)
            {
                owner.Transition<RangedChaseState>();
            }
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
        base.Exit();
    }
}

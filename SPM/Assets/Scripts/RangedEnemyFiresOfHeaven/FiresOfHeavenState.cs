using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ranged/FiresOfHeavenState")]
public class FiresOfHeavenState : RangedBaseState
{
    [SerializeField]
    private int numberOfCasts;
    [SerializeField]
    private float startingHeight;
    [SerializeField]
    private GameObject fireBallObject;
    [SerializeField]
    private float castTime = 1f;
    private int currentCastCount;
    private float coolDown;
    private bool isTransitioning;
    private float reCastSpeed = 2;
    private bool isRecharging;
   
    public override void Enter()
    {
        fireBallObject.GetComponent<FireBall>().parent = owner.firesOfHeavenContainer.transform;
        owner.anim.SetBool("FireRain", true);
        if (owner.NavAgent.isActiveAndEnabled)
            owner.NavAgent.isStopped = true;
        currentCastCount = 0;
        reCastSpeed = 2;
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
            cooldown = castTime;
        }
        if (isRecharging)
        {
            reCastSpeed -= Time.deltaTime;
        }
        base.HandleUpdate();
            
    }

    private void SpawnFire()
    {
        if (currentCastCount < numberOfCasts)
        {
            Vector3 vec = SpawnNearPlayer(1.5f);
            GameObject obj = Instantiate(fireBallObject, vec, Quaternion.identity, owner.firesOfHeavenContainer.transform);

            Destroy(obj, 8f);
            currentCastCount++;
        }
        else
        {
            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > owner.lostTargetDistance)
            {
                owner.Transition<RangedEnemyPatrolState>();
            }

            isRecharging = true;
            if (reCastSpeed < 0)
            {
                owner.Transition<RangedChaseState>();
            }
        }
    }

    public Vector3 SpawnNearPlayer(float distance)
    {
        float max = distance;
        float min = -distance;
        return new Vector3(Random.Range(min, max) + owner.player.transform.position.x, owner.player.transform.position.y + startingHeight, Random.Range(min, max) + owner.player.transform.position.z);
    }

    public override void Exit()
    {
        owner.anim.SetBool("FireRain", false);
        base.Exit();
    }
}

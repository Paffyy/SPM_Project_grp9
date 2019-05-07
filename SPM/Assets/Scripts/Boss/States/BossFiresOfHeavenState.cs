using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/FiresOfHeavenState")]
public class BossFiresOfHeavenState : BossBaseState
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
        owner.FireEffectOnBoss.SetActive(true);
        owner.NavAgent.isStopped = true;
        base.Enter();
        //förbereder attacken
        fireArea = owner.FireArea.GetComponent<BoxCollider>();
         minX = fireArea.transform.position.x - (fireArea.size.x / 2);
         maxX = fireArea.transform.position.x + (fireArea.size.x / 2);
         maxZ = fireArea.transform.position.z - (fireArea.size.z / 2);
         minZ = fireArea.transform.position.z + (fireArea.size.z / 2);
        //owner.StartCoroutine(FiresOfHeavenState());
        //vecArr = CreateRandomVectors();
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
            //Debug.Log(owner.FireArea.transform.position + vecArr[count] + " " + count);
            //GameObject obj = GameObject.Instantiate(FireBall, owner.FireArea.transform.position + vecArr[count] + (Vector3.up * 50), Quaternion.identity);

            //spawnar innom ett område
            //Vector3 vec = new Vector3(Random.Range(minX, maxX), fireArea.transform.position.y + StartingHeight, Random.Range(minZ, maxZ));

            Vector3 vec = SpawnNearPlayer();


            GameObject obj = GameObject.Instantiate(FireBall, vec, Quaternion.identity);
            obj.transform.SetParent(owner.FireArea.transform);
            count++;
        }
        else
        {

            owner.Transition<BossAttackState>();
        }
    }

    public Vector3 SpawnNearPlayer()
    {
        float max = 2.0f;
        float min = -2.0f;

        return new Vector3(Random.Range(min, max) + owner.player.transform.position.x, fireArea.transform.position.y + StartingHeight, Random.Range(min, max) + owner.player.transform.position.z);
    }

    public override void Exit()
    {
        owner.FireEffectOnBoss.SetActive(false);
        owner.anim.SetBool("FireRain", false);
        owner.NavAgent.isStopped = false;
        base.Exit();
    }

    //private IEnumerator FiresOfHeavenState()
    //{
    //    Vector3[] arr = CreateRandomVectors();
    //    Debug.Log(arr.ToString());
    //    Debug.Log(arr.Length);

    //    //förbereder attacken
    //    yield return new  WaitForSeconds(1);
    //    Debug.Log("boop");
    //     foreach(Vector3 vec in arr)
    //    {
    //        GameObject.Instantiate(fireArea, vec, Quaternion.identity, owner.FireArea.transform);
    //        Debug.Log("drop " + vec);
    //        yield return new WaitForSeconds(Time);
    //    }

    //    //efter attacken  chilling
    //    yield return new WaitForSeconds(3);

    //    owner.Transition<BossAttackState>();

    //}

    //private Vector3[] CreateRandomVectors()
    //{
    //    float minX = fireArea.transform.position.x - (fireArea.size.x / 2);
    //    float maxX = fireArea.transform.position.x + (fireArea.size.x / 2);
    //    float maxZ = fireArea.transform.position.z - (fireArea.size.z / 2);
    //    float minZ = fireArea.transform.position.z + (fireArea.size.z / 2);


    //    Vector3[] arr = new Vector3[NumberOfFires];
    //    for (int i = 0; i > NumberOfFires; i++)
    //    {
    //        Debug.Log("creating vector " + i + arr[i]);
    //        arr[i] = new Vector3(Random.Range(minX, maxX), StartingHeight, Random.Range(maxZ, minZ));
    //        //arr[i] = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), StartingHeight);
    //    }

    //    return arr;
    //}
}

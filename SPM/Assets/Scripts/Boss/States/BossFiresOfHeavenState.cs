using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/FiresOfHeavenState")]
public class BossFiresOfHeavenState : BossBaseState
{
    public int NumberOfFires;
    private RectTransform fireArea;
    public float StartingHeight;
    public GameObject FireBall;
    //tid mellan eldbollar
    public float BetweenTime = 0.5f;

    private Vector3[] vecArr;
    private int count = 0;
    private float coolDown;
    public override void Enter()
    {
        base.Enter();
        //förbereder attacken
        fireArea = owner.FireArea.GetComponent<RectTransform>();
        //owner.StartCoroutine(FiresOfHeavenState());
        vecArr = CreateRandomVectors();
    }
    public override void HandleUpdate()
    {
        cooldown -= Time.deltaTime;
        if(cooldown < 0)
        {
            if(count < vecArr.Length)
            {
            //Debug.Log(owner.FireArea.transform.position + vecArr[count] + " " + count);
            GameObject obj = GameObject.Instantiate(FireBall, owner.FireArea.transform.position + vecArr[count] + (Vector3.up * 50), Quaternion.identity);
            obj.transform.SetParent(owner.FireArea.transform);
            count++;
            }
            else
                owner.Transition<BossAttackState>();

            cooldown = BetweenTime;
        }

        base.HandleUpdate();
            
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

    private Vector3[] CreateRandomVectors()
    {
        Debug.Log("boopus");

        float minX = fireArea.rect.xMin;
        float maxX = fireArea.rect.xMax;
        float minY = fireArea.rect.yMin;
        float maxY = fireArea.rect.yMax;

        Vector3[] arr = new Vector3[NumberOfFires];
        for (int i = 0; i > NumberOfFires; i++)
        {
            Debug.Log("creating vector " + i + arr[i]);
            arr[i] = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), StartingHeight);
        }

        return arr;
    }
}

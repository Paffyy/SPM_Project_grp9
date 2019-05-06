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
    public float Time;

    public override void Enter()
    {
        base.Enter();
        //förbereder attacken
        fireArea = owner.FireArea.GetComponent<RectTransform>();
        owner.StartCoroutine(FiresOfHeavenState());
    }
    public override void HandleUpdate()
    {
        base.HandleUpdate();
    }

    private IEnumerator FiresOfHeavenState()
    {
        Vector3[] arr = CreateRandomVectors();
        Debug.Log(arr.Length);

        //förbereder attacken
        yield return new  WaitForSeconds(1);

        for (int i = 0; i > arr.Length; i++)
        {
            GameObject.Instantiate(fireArea, arr[i], Quaternion.identity, owner.FireArea.transform);
            Debug.Log("drop " + i);
            yield return new WaitForSeconds(Time);
        }

        //efter attacken  chilling
        yield return new WaitForSeconds(3);

        owner.Transition<BossAttackState>();

    }

    private Vector3[] CreateRandomVectors()
    {

        float minX = fireArea.rect.xMin;
        float maxX = fireArea.rect.xMax;
        float minY = fireArea.rect.yMin;
        float maxY = fireArea.rect.yMax;

        Vector3[] arr = new Vector3[NumberOfFires];
        for (int i = 0; i > NumberOfFires; i++)
        {
            arr[i] = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), StartingHeight);
        }

        return arr;
    }
}

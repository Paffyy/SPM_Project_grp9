using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/FiresOfHeavenState")]
public class BossFiresOfHeavenState : BossBaseState
{
    public int NumberOfFires;
    private GameObject fireArea;
    public float StartingHeight;
    public GameObject FireBall;
    //tid mellan eldbollar
    public float Time;

    public override void Enter()
    {
        base.Enter();
        //förbereder attacken
        fireArea = owner.FireArea;
        owner.StartCoroutine(FiresOfHeavenState());
    }
    public override void HandleUpdate()
    {
        base.HandleUpdate();
    }

    private IEnumerator FiresOfHeavenState()
    {
        Vector3[] arr = CreateRandomVectors();

        //förbereder attacken
        yield return new  WaitForSeconds(1);

        for (int i = 0; i > arr.Length; i++)
        {
            GameObject.Instantiate(fireArea, arr[i], Quaternion.identity, fireArea.transform);
            yield return new WaitForSeconds(Time);
        }

        //efter attacken  chilling
        yield return new WaitForSeconds(1);

    }

    private Vector3[] CreateRandomVectors()
    {
        RectTransform rt = (RectTransform)fireArea.transform;
        float minX = rt.rect.xMin;
        float maxX = rt.rect.xMax;
        float minY = rt.rect.yMin;
        float maxY = rt.rect.yMax;

        Vector3[] arr = new Vector3[NumberOfFires];
        for (int i = 0; i > NumberOfFires; i++)
        {
            arr[i] = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), StartingHeight);
        }

        return arr;
    }
}

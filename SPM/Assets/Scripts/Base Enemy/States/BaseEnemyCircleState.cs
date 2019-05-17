using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/CircleState")]
public class BaseEnemyCircleState : BaseEnemyBaseState
{
    private float circleDistance;
    //private float angle;
    //[SerializeField] public float NextAnglePoint;
    [SerializeField] private float timeBetweenAngels;
    private float timer;
    private Vector3 targetPos;
    private List<Vector3> listOfPoints;
    private int curentCount;
    private int maxSteps = 3;

    private bool isStandningStill;
    private float maxStandningStillTime = 0.3f;
    private float stillCount;

    private float minDistance = 1.0f;

    public override void Enter()
    {
        curentCount = 0;
        timer = timeBetweenAngels;
        //angle = Vector3.Angle(owner.player.transform.position, owner.transform.position);
        Debug.Log("CircleState");
        Vector3 dis = owner.player.transform.position - owner.transform.position;
        circleDistance = Vector3.SqrMagnitude(dis);
        CreateListOfPoints();
        base.Enter();
    }

    private void CreateListOfPoints()
    {
        int rand = Random.Range(1, 3);
        bool goLeft = rand == 1 ? true : false;
        Debug.Log("goLeft " + goLeft + " rand " + rand);
        listOfPoints = Manager.Instance.GetFlankingPoints(owner.transform, owner.player.transform, circleDistance, 15.0f, goLeft);
    }

    private void standningStill()
    {

    }

    public override void HandleUpdate()
    {


        timer -= Time.deltaTime;
        //owner.transform.LookAt(owner.player.transform.position);

        Vector3 disVector = owner.player.transform.position - owner.transform.position;
        float dis = Vector3.SqrMagnitude(disVector);
        if (dis < minDistance)
        {
            Debug.Log("mindis");
            owner.Transition<BaseEnemyAttackState>();
        }
        if(owner.controller.Velocity == Vector3.zero)
        {
            stillCount += Time.deltaTime;
            if(stillCount > 0)
            {
                isStandningStill = true;
            }
        }
        if(owner.controller.Velocity != Vector3.zero)
        {
            stillCount = maxStandningStillTime;
        }

        if (curentCount > maxSteps || curentCount > listOfPoints.Count)
            owner.Transition<BaseEnemyAttackState>();

        if (timer < 0)
        {
            Circle();
        }
        Debug.DrawLine(owner.player.transform.position, listOfPoints[curentCount]);
        base.HandleUpdate();
    }

    private void Circle()
    {
        UpdateDestination(listOfPoints[curentCount]);
        //NextAnglePoint += NextAnglePoint;
        curentCount++;
        timer = timeBetweenAngels;
    }
}

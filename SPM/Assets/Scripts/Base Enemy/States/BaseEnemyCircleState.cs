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
    private float timerSteps;
    private Vector3 targetPos;
    private List<Vector3> listOfPoints;
    private int curentCount;
    private int maxSteps = 4;

    private float maxStandningStillTime = 1.0f;
    private float stillCount;

    private float maxTimeInState = 3.0f;
    private float timerInState;

    private float minDistance = 0.3f;

    public override void Enter()
    {
        curentCount = 0;
        timerSteps = timeBetweenAngels;
        timerInState = maxTimeInState;
        Debug.Log("CircleState");
        Vector3 disVector = owner.player.transform.position - owner.transform.position;
        circleDistance = disVector.magnitude;
        if (circleDistance < 4.0f)
            circleDistance = 4.0f;

        CreateListOfPoints();
        base.Enter();
    }

    public override void HandleUpdate()
    {
        timerInState -= Time.deltaTime;
        if (timerInState < 0)
        {
            Debug.Log("GoBack");
            owner.Transition<BaseEnemyAttackState>();
        }


        UpdateRotation(owner.player.transform);
        ToNextPoint();
        CheckStandingStill();
        CheckIfTooClose();
        Debug.DrawLine(owner.player.transform.position, listOfPoints[curentCount]);
        base.HandleUpdate();
    }

    private void CreateListOfPoints()
    {
        int rand = Random.Range(1, 3);
        bool goLeft = rand == 1 ? true : false;
        //Debug.Log("goLeft " + goLeft + " rand " + rand);
        List<Vector3> flankList = Manager.Instance.GetFlankingPoints(owner.transform, owner.player.transform, circleDistance, 30.0f, goLeft);
        listOfPoints = flankList;

        //Debug ritar ut svärer med OnGizmoSeleced()
        owner.DrawList.Clear();
        owner.DrawList = listOfPoints;
    }

    private void ToNextPoint()
    {
        timerSteps -= Time.deltaTime;

        if (timerSteps < 0)
        {

            Circle();
            timerSteps = timeBetweenAngels;
        }
    }

    private void CheckStandingStill()
    {
        if (owner.controller.Velocity == Vector3.zero)
        {
            stillCount -= Time.deltaTime;
            if (stillCount > 0)
            {
                Debug.Log("is stadning still");
                owner.Transition<BaseEnemyAttackState>();
            }
        }
        else
        {
            stillCount = maxStandningStillTime;
        }
    }

    private void CheckIfTooClose()
    {
        Vector3 disVector = owner.player.transform.position - owner.transform.position;
        float dis = disVector.sqrMagnitude;
        if (dis < minDistance * minDistance)
        {
            Debug.Log("mindis " + (dis < minDistance));
            owner.Transition<BaseEnemyAttackState>();
        }
    }

    private void Circle()
    {
        if (curentCount > maxSteps || curentCount > listOfPoints.Count)
        {
            Debug.Log("out of points");
            owner.Transition<BaseEnemyAttackState>();
        }

        UpdateDestination(listOfPoints[curentCount]);
        //NextAnglePoint += NextAnglePoint;
        Debug.Log(curentCount + " currentcount" );
        curentCount++;

    }


}

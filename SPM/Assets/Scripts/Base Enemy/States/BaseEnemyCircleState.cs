using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/CircleState")]
public class BaseEnemyCircleState : BaseEnemyBaseState
{
    private float circleDistance;
    private float angle;
    [SerializeField] public float NextAnglePoint;
    [SerializeField] private float timeBetweenAngels;
    private float timer;
    private Vector3 targetPos;

    public override void Enter()
    {
        timer = timeBetweenAngels;
        circleDistance = Vector3.Distance(owner.player.transform.position, owner.transform.position);
        angle = Vector3.Angle(owner.player.transform.position, owner.transform.position);
        Debug.Log("CircleState");
        base.Enter();
    }
    public override void HandleUpdate()
    {
        Vector3 dis = owner.player.transform.position - owner.transform.position;
        circleDistance = Vector3.SqrMagnitude(dis);
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Circle();
        }
        base.HandleUpdate();
    }

    private void Circle()
    {
        Debug.DrawLine(owner.player.transform.position, FindNewPosition());
        owner.UpdateDestination(Vector3.ProjectOnPlane(FindNewPosition(), Vector3.up));
        NextAnglePoint += NextAnglePoint;
        timer = timeBetweenAngels;
    }

    private Vector3 FindNewPosition()
    {
        float x = circleDistance * Mathf.Cos(angle+NextAnglePoint * Mathf.Deg2Rad) + targetPos.x;
        float z = circleDistance * Mathf.Sin(angle+NextAnglePoint * Mathf.Deg2Rad) + targetPos.z;
        Debug.Log("distance " + circleDistance + "angle " + angle * Mathf.Deg2Rad + " new angle " + angle + NextAnglePoint * Mathf.Deg2Rad);

        return new Vector3(x, 0, z);
    }

        //private Vector3 PlayerCircle()
        //{
        //    float xPos = owner.player.transform.position.x + circleDistance * Mathf.Cos(angle * Mathf.Deg2Rad);

        //    float zPos = owner.player.transform.position.z + circleDistance * Mathf.Sin(angle * Mathf.Deg2Rad);


        //    Vector3 newPos = new Vector3(xPos, zPos);
        //    return newPos;
        //}

        //private void Circle()
        //{
        //    Debug.Log("circle " + angle);
        //    owner.UpdateDestination(PlayerCircle(), 0.5f);
        //    angle += NextAnglePoint;
        //    timer = timeBetweenAngels;
        //}




    }

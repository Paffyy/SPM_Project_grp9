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
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Circle();
        }
        base.HandleUpdate();
    }

    private Vector3 PlayerCircle()
    {
        float xPos = owner.player.transform.position.x + circleDistance * Mathf.Cos(angle * Mathf.Deg2Rad);

        float zPos = owner.player.transform.position.z + circleDistance * Mathf.Sin(angle * Mathf.Deg2Rad);


        Vector3 newPos = new Vector3(xPos, zPos);
        return newPos;
    }

    private void Circle()
    {
        Debug.Log("circle " + angle);
        owner.UpdateDestination(PlayerCircle(), 0.5f);
        angle += NextAnglePoint;
        timer = timeBetweenAngels;
    }


}

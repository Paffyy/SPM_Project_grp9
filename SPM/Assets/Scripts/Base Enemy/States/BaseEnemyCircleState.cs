using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseEnemy/CircleState")]
public class BaseEnemyCircleState : BaseEnemyBaseState
{
    public float circleDistance;
    private float angle;
    public float NextAnglePoint;
    public override void Enter()
    {
        circleDistance = Vector3.Distance(owner.player.transform.position, owner.transform.position);
        angle = Vector3.Angle(owner.player.transform.position, owner.transform.position);
        Debug.Log("CircleState");
        base.Enter();
    }
    public override void HandleUpdate()
    {
        owner.StartCoroutine(CirlcleIEnum());
        base.HandleUpdate();
    }

    private Vector3 PlayerCircle()
    {
        float xPos = owner.player.transform.position.x + circleDistance * Mathf.Cos(angle);
        float zPos = owner.player.transform.position.z + circleDistance * Mathf.Sin(angle);

        Vector3 newPos = new Vector3(xPos, zPos);
        return newPos;
    }

    private IEnumerator CirlcleIEnum()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("circle");
        owner.UpdateDestination(PlayerCircle(), 0.5f);
        angle += NextAnglePoint;
    }
}

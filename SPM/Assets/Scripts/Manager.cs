using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager
{
    private Manager() {  }
    private static Manager instance;
    private Vector3 checkPoint;

    public static Manager Instance
    {
        get
        {
            if (instance == null)
                instance = new Manager();
            return instance;
        }
    }

    public List<Collider> GetFrontConeHit(Vector3 facingDirection, Transform transform, LayerMask layerMask, float radius, float angle)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, layerMask).ToList() ?? new List<Collider>();
        for (int i = colliders.Count - 1; i >= 0; i--)
        {
            var direction = (colliders[i].transform.position - transform.position).normalized;
            if (!(Vector3.Angle(facingDirection, direction) < angle / 2))
            {
                // TODO can hit through walls
                colliders.Remove(colliders[i]);
            }
        }
        return colliders;
    }
    public List<Collider> GetAoeHit(Vector3 pos, LayerMask layerMask, float radius)
    {
        var colliders = Physics.OverlapSphere(pos, radius, layerMask).ToList() ?? new List<Collider>();
        return colliders;
    }
    public Vector3 GetInitialVelocity(Vector3 pos, Vector3 target, float timeToTarget, int angleInDegrees, float gravityForce)
    {
        var radialAngle = angleInDegrees * Mathf.PI / 180f;
        var distance = Vector3.Distance(pos, target);
        var direction = (target - pos).normalized + new Vector3(0, Mathf.Cos(radialAngle), 0);
        var initialVelocity = Mathf.Sqrt((distance * gravityForce) / Mathf.Sin(2 * radialAngle));
        return direction.normalized * initialVelocity;
    }
    public Vector3 GetInitialVelocity2(Vector3 pos, Vector3 target, float gravityForce)
    {
        var heightDifference = target.y - pos.y;
        var distanceXZ = new Vector3(target.x - pos.x, 0, target.z - pos.z);
        var arcHeight = Mathf.Abs(heightDifference) + distanceXZ.magnitude / 10;
        var velocityY = Vector3.up * Mathf.Sqrt(-2 * gravityForce * arcHeight);
        var velocityXZ = distanceXZ / (Mathf.Sqrt(-2 * arcHeight / gravityForce) + Mathf.Sqrt(2 * (heightDifference - arcHeight) / gravityForce));
        return velocityXZ + velocityY;
    }

    public List<Vector3> GetRandomPointsInAreaXZ(Vector3 position,int count, float radius)
    {
        List<Vector3> listOfRandomPoints = new List<Vector3>();
        for (int i = 0; i < count-1; i++)
        {
            listOfRandomPoints.Add(new Vector3(Random.Range(-radius, +radius),0, Random.Range(-radius, +radius)) + position);
        }
        return listOfRandomPoints;
    }
    public List<Vector3> GetRandomPointsInAreaXYZ(Vector3 position, int aimingOffset , int count, float radius)
    {
        List<Vector3> listOfRandomPoints = new List<Vector3>();
        for (int i = 0; i < count - 1; i++)
        {
            listOfRandomPoints.Add(new Vector3(Random.Range(-radius, +radius), Random.Range(-radius, +radius), Random.Range(-radius, +radius)) + (position * aimingOffset));
        }
        return listOfRandomPoints;
    }

    public Vector3 GetCheckPoint()
    {
        return checkPoint;
    }

    public void SetCheckPoint(Vector3 position)
    {
        checkPoint = position;
    }

}

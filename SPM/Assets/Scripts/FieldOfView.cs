using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask TargetsToHit;

    public Vector3 DirectionFromAngle(float angle, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public GameObject[] TargetsInFieldOfView()
    {
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, viewRadius, TargetsToHit);
        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                objs.Add(target.gameObject);

            }
        }
        return objs.ToArray();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, viewRadius);
        //Gizmos.DrawFrustum(transform.position, viewAngle, viewRadius, 0.0f, 1.0f);
        float totalFOV = viewAngle;
        float rayRange = viewRadius;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
        //Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

}

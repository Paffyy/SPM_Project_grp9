using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private GameObject vfx;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;

    void Start()
    {
        Vector3 startPos = startPoint.position;
        GameObject objVFX = Instantiate(vfx, startPos, Quaternion.identity) as GameObject;
        Vector3 endPos = endPoint.position;
        RotateTowards(objVFX, endPos);
    }

    void Update()
    {
        
    }

    void RotateTowards(GameObject obj, Vector3 destination)
    {
        Vector3 direction = destination - obj.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
}

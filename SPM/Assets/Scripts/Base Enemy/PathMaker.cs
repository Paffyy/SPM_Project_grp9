using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaker : MonoBehaviour
{
    public Color rayColor = Color.blue;
    public List<Transform> PathObjects = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Transform[] arr = GetComponentsInChildren<Transform>();
        PathObjects.Clear();

        foreach(Transform obj in arr)
        {
            if(obj != this.transform)
            {
                PathObjects.Add(obj);
            }
        }

        for (int i = 0; i < PathObjects.Count; i++)
        {
            Vector3 position = PathObjects[i].position;
            Gizmos.DrawWireSphere(PathObjects[i].position, 0.3f);
            if (i > 0)
            {
                Gizmos.DrawLine(PathObjects[i - 1].position, PathObjects[i].position);
                
            }
        }

    }
}

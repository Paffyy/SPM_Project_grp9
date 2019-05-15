using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAreaRevitalize : MonoBehaviour
{
    [SerializeField]
    [Range(0,100)]
    private float Radius, Falloff;
    private void Update()
    {
        Shader.SetGlobalVector("_Position", transform.position);
        Shader.SetGlobalFloat("_Radius", Radius);
        Shader.SetGlobalFloat("_Falloff", Falloff);
    }

}

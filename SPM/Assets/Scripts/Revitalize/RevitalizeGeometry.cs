using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeGeometry : MonoBehaviour
{
    public bool IsRevitalized;
    public virtual void Revitalize(float offset = 0) { }
    public virtual void InstantRevitalize() { }
}

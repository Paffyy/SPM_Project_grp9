using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeObjective : MonoBehaviour
{
    public bool IsCompleted;
    
    public void SetCompleted(bool toggle = true)
    {
        IsCompleted = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCrosshair : MonoBehaviour
{
    public GameObject Crosshair;
    private bool isAiming;
    
    public void ToggleCrosshair(bool toggle)
    {
        isAiming = toggle;
        Crosshair.SetActive(toggle);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCrosshair : MonoBehaviour
{
    public GameObject Crosshair;
    private bool isAiming;
    void Start()
    {
        Crosshair.SetActive(false);
    }
    void LateUpdate()
    {
        if (isAiming)
        {
            Crosshair.SetActive(true);
        }
        else
        {
            Crosshair.SetActive(false);
        }
    }

    public void ToggleCrosshair(bool toggle)
    {
        isAiming = toggle;
        Crosshair.SetActive(toggle);
    }
}

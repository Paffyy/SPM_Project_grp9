using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCrosshair : MonoBehaviour
{
    public GameObject Crosshair;
    public GameObject thirdPersonCamera;
    private Vector3 cameraOffset;
    private bool isAiming;
    // Start is called before the first frame update
    void Start()
    {
        ToggleCrosshair(false);
        cameraOffset = new Vector3(1, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            UpdateRotation();
            UpdatePosition();
        }
      
    }

    public void ToggleCrosshair(bool toggle)
    {
        isAiming = toggle;
        Crosshair.SetActive(toggle);
    }
    public void PositionCrosshair()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        Crosshair.transform.position = hit.point;
        Crosshair.transform.LookAt(transform);
    }
    private void UpdateRotation(float swing = 0)
    {

    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * cameraOffset.normalized;
        thirdPersonCamera.transform.position = update * cameraOffset.magnitude + transform.position;
    }
}

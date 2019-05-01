using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCrosshair : MonoBehaviour
{
    public GameObject Crosshair;
    public GameObject thirdPersonCamera;
    public LayerMask CollisionMask;
    public LayerMask CrosshairLayerMask;
    public Player player;
    public GameObject WorldCrosshair;
    private bool inRange;
    private bool isAiming;
    private float chargeTime = 0.1f;
    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCamera.SetActive(false);
        Crosshair.SetActive(false);
        WorldCrosshair.SetActive(false);
        cameraPosition = new Vector3(0.0f, 0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleThirdPersonCamera();
    }
    void LateUpdate()
    {
        if (isAiming)
        {
            if (inRange)
            {
                Crosshair.SetActive(false);
                WorldCrosshair.SetActive(true);
            }
            else
            {
                Crosshair.SetActive(true);
                WorldCrosshair.SetActive(false);
            }
            if (chargeTime < 1.5f)
            {
                chargeTime += Time.deltaTime;
            }
            thirdPersonCamera.SetActive(true);
            cameraPosition = new Vector3(0.8f, 0.2f, -2.5f);
        }
        else
        {
            chargeTime = 0.1f;
            thirdPersonCamera.SetActive(false);
            cameraPosition = new Vector3(0.0f, 0.5f, 0.0f);
            Crosshair.SetActive(false);
            WorldCrosshair.SetActive(false);
        }
    }
    protected virtual void HandleThirdPersonCamera()
    {
        RaycastHit hit;
        Vector3 cameraUpdate = thirdPersonCamera.transform.rotation * cameraPosition.normalized;
        if (Physics.SphereCast(player.transform.position, player.GetComponentInChildren<SphereCollider>().radius, cameraUpdate, out hit, cameraPosition.magnitude, CollisionMask))
        {
            Vector3 newPosition = cameraUpdate * (hit.distance - player.GetComponentInChildren<SphereCollider>().radius);
            thirdPersonCamera.transform.position = newPosition + player.transform.position;
        }
        else
        {
            thirdPersonCamera.transform.position = cameraUpdate * cameraPosition.magnitude + GetComponentInParent<Transform>().position;
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
        Physics.Raycast(thirdPersonCamera.transform.position, thirdPersonCamera.transform.forward, out hit, GetComponent<Bow>().MaxArrowDistance * (chargeTime / 1.5f), CrosshairLayerMask);
        if (hit.collider != null)
        {
            WorldCrosshair.transform.position = hit.point;
            WorldCrosshair.transform.LookAt(thirdPersonCamera.transform);
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
}

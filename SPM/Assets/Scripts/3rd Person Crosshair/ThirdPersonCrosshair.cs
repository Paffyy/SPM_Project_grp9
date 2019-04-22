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
    private bool isAiming;
    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCamera.SetActive(false);
        ToggleCrosshair(false);
        cameraPosition = new Vector3(0.0f, 0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            thirdPersonCamera.SetActive(true);
            cameraPosition = new Vector3(0.8f, 0.2f, -2.5f);
        }
        else
        {
            thirdPersonCamera.SetActive(false);
            cameraPosition = new Vector3(0.0f, 0.5f, 0.0f);
        }
        HandleThirdPersonCamera();

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
        Physics.Raycast(transform.position, transform.forward, out hit, 50, CrosshairLayerMask);
        Crosshair.transform.position = hit.point;
        Crosshair.transform.LookAt(thirdPersonCamera.transform);
    }
}

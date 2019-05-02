using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform PlayerCamera;
    public Transform Player;
    public Transform Portal;
    public Transform OtherPortal;


    // Update is called once per frame
    void Update()
    {
        //Vector3 playerOffsetFromPortal = Player.position - OtherPortal.position;
        //transform.position = Portal.position + playerOffsetFromPortal;
        //float angularDifferenceBetweenPortalRotations = Quaternion.Angle(Portal.rotation, OtherPortal.rotation);
        //Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        //Vector3 newCameraDirection = portalRotationalDifference * PlayerCamera.forward;
        //transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }

    private void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = Player.position - OtherPortal.position;
        transform.position = Portal.position + playerOffsetFromPortal;
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(Portal.rotation, OtherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * PlayerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}

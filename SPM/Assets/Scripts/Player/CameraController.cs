using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distanceFromTarget;
    private float pitchMinAngle = -40;
    private float pitchMaxAngle = 85;
    private float yaw;
    public float Yaw { get { return yaw;} }
    private float pitch;
    public float Pitch { get { return pitch;} }
    private Vector3 rotationSmoothVel;
    private Vector3 currentRotation;
    public float MouseSensitivity;
    public float rotationSmoothTime;

    private Vector3 cameraPosition;
    private SphereCollider sphere;
    [SerializeField] private LayerMask CollisionMask;
    public bool FirstPersonView = false;

    //protected Vector3 cameraPosition;
    //protected SphereCollider sphere;
    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        cameraPosition = new Vector3(0, target.rotation.y, target.rotation.z);
    }


    void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        // Z-axeln
        yaw += Input.GetAxis("Mouse X") * MouseSensitivity;

        // X-axeln
        //Ändra pitchen till += om du vill ha den inverterad
        pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinAngle, pitchMaxAngle);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVel, rotationSmoothTime);

        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;

        if (Input.GetKeyDown(KeyCode.V))
            FirstPersonView = !FirstPersonView;
        if (FirstPersonView)
        {
            HandleFirstPersonCamera();
        }
        else
        {
            HandleThirdPersonCamera();
        }

    }

    private void HandleFirstPersonCamera()
    {
        transform.position = target.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
    }

    private void HandleThirdPersonCamera()
    {
        RaycastHit hit;
        //Vector3 cameraUpdate = transform.rotation * cameraPosition.normalized;
        Vector3 cameraUpdate = target.position - transform.forward * distanceFromTarget;
        bool intersect = Physics.SphereCast(transform.position, sphere.radius, cameraUpdate, out hit, cameraPosition.magnitude, CollisionMask);
        if (intersect)
        {
            Vector3 newPosition = cameraUpdate * (hit.distance - sphere.radius);
            transform.position = newPosition + target.transform.position;
        }
        else
        {
            //transform.position = cameraUpdate * cameraPosition.magnitude + target.transform.position;
            transform.position = cameraUpdate;

        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawSphere((target.position - transform.forward * distanceFromTarget), sphere.radius);
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTest : MonoBehaviour
{
    private SimpelCharacterController controller;
    private CameraController cameraCon;

    float inputRotationX;
    float inputRotationY;

    Quaternion rotation;

    //[SerializeField] private float turnSmoothVel;
    //[SerializeField] private float turnSmoothTime;

    private float rotationSpeed = 0.2f;
    //[SerializeField] private float speedSmoothTime;
    //[SerializeField] private float speedSmothVelocity;
    public float MovementSpeed;
    private float currentSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<SimpelCharacterController>();
        cameraCon = GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        //Vector3 input = direction.normalized;
        //Debug.DrawRay(transform.position, input, Color.red);
        //Debug.Log(input);

        //float targetSpeed = MovementSpeed * input.magnitude;
        //currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmothVelocity, speedSmoothTime);
        //transform.Translate(Vector3.ProjectOnPlane(transform.forward,Vector3.up) * currentSpeed * Time.deltaTime, Space.World);

        inputRotationX = (inputRotationX + Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime) % 360;
        inputRotationY = Mathf.Clamp(inputRotationY - Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime, -88, 88);


        //rotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, ))



        //Om spelaren rör på sig, ska spelaren rotera efter kamerans rotation
        //if (input != Vector3.zero)
        //{
        //    //float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraCon.transform.eulerAngles.y;
        //    //transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVel, turnSmoothTime);
        //    Rotate();

        //}

        //transform.rotation = cameraCon.transform.rotation;

        //camera.transform.rotation = Quaternion.Euler(RotationX, RotationY, 0.0f);
        //if (input == Vector3.zero)
        //controller.MoveRotation(cameraCon.transform.forward, 0.0f);



    }

    void Rotate()
    {
        Vector3 targetDir = transform.forward - (-cameraCon.transform.forward);
        Vector3 modifiedDir = new Vector3(targetDir.x, 0, targetDir.z);

        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, modifiedDir, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTest : MonoBehaviour
{
    private CharacterController controller;
    private CameraController cameraCon;

    [SerializeField] private float turnSmoothVel;
    [SerializeField] private float turnSmoothTime;

    [SerializeField] private float speedSmoothTime;
    [SerializeField] private float speedSmothVelocity;
    public float MovementSpeed;
    private float currentSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraCon = GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 input = direction.normalized;
        float targetSpeed = MovementSpeed * input.magnitude;
        Debug.Log(input);
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmothVelocity, speedSmoothTime);
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);


        //Om spelaren rör på sig, ska spelaren rotera efter kamerans rotation
        if (input != Vector3.zero)
        {
        float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraCon.transform.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVel, turnSmoothTime);
        }

        //transform.rotation = cameraCon.transform.rotation;

        //camera.transform.rotation = Quaternion.Euler(RotationX, RotationY, 0.0f);
        //if (input == Vector3.zero)
        //controller.MoveRotation(cameraCon.transform.forward, 0.0f);



    }
}

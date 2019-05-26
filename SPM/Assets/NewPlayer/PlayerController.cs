using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PhysicsController phyController;
    private CameraController cameraCon;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float fallingExtraSpeed = 2.0f;
    [SerializeField] private float rotationSpeed;
    public float MovementSpeed;
    private float currentSpeed;
    [SerializeField] private float accelaration;
    private Vector3 currentVel;
    private Vector3 currentRotationTarget;

    // Start is called before the first frame update
    void Awake()
    {
        phyController = GetComponent<PhysicsController>();
        cameraCon = GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.Instance.IsPaused == false)
        {

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            if (phyController.IsGrounded() == true) {
                if (InputManager.Instance.GetkeyDown((KeybindManager.Instance.Jump), InputManager.ControllMode.Play))
                {
                    Jump();
                }
            }
            else
            {
                Fall();
            }
            Move(input);

            //om spelaren inte rör sig tillåt kameran att rotera runt den
            if (input.x != 0 || input.z != 0)
            {
                //Om det blir något knas med spelarens rotation lägg till offsetHelper. Den ser till så att man inte kollar SignedAngle mot inputvector som skulle vara 0
                //Vector3 offsetHelper = transform.forward * 0.001f;
                //transform.rotation = Quaternion.AngleAxis(cameraCon.Yaw + Vector3.SignedAngle(Vector3.forward, input.normalized , Vector3.up), Vector3.up);
                UpdateRotationDirection();
            }
            Rotate();

        }


    }


    private void Jump()
    {
        phyController.Velocity += Vector3.up * jumpHeight;
    }

    private void Fall()
    {
        phyController.Velocity += Vector3.down * fallingExtraSpeed;
    }

    private void Move(Vector3 input)
    {
        currentVel = Quaternion.AngleAxis(cameraCon.Yaw, Vector3.up) * input * MovementSpeed;

        Debug.DrawRay(transform.position, currentVel, Color.red);


        phyController.Velocity = new Vector3(currentVel.x, phyController.Velocity.y, currentVel.z);

    }

    void UpdateRotationDirection()
    {
        //spelaren roterar åt den riktning den går mot
        //Vector3 targetDir = transform.forward - (-currentVel);

        //Spelaren roterar åt kamerans håll

        //Vector3 targetDir = transform.forward - (-cameraCon.transform.forward);
        Vector3 targetDir = cameraCon.transform.forward;
        Vector3 modifiedDir = new Vector3(targetDir.x, 0, targetDir.z);
        currentRotationTarget = modifiedDir;
    }

    void Rotate()
    {

        Debug.DrawRay(transform.position, currentRotationTarget, Color.green);
        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, currentRotationTarget, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }


}

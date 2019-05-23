using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PhysicsController controller;
    private CameraController cameraCon;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float rotationSpeed;
    public float MovementSpeed;
    private float currentSpeed;

    private Vector3 currentVel;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PhysicsController>();
        cameraCon = GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.Instance.IsPaused == false)
        {

            Vector3 input = Vector3.zero;
            if (controller.IsGrounded() == true) {
                Debug.Log("isGrounded");
                if (InputManager.Instance.GetkeyDown((KeybindManager.Instance.Jump), InputManager.ControllMode.Play))
                {
                    Jump();
                }
            }
            input = Move();

            //om spelaren inte rör sig tillåt kameran att rotera runt den
            if (input.x != 0 || input.z != 0)
            {
                //Om det blir något knas med spelarens rotation lägg till offsetHelper. Den ser till så att man inte kollar SignedAngle mot inputvector som skulle vara 0
                //Vector3 offsetHelper = transform.forward * 0.001f;
                //transform.rotation = Quaternion.AngleAxis(cameraCon.Yaw + Vector3.SignedAngle(Vector3.forward, input.normalized , Vector3.up), Vector3.up);
                Rotate();
            }

        }


    }


    private void Jump()
    {
        controller.Velocity += Vector3.up * jumpHeight;
    }

    private Vector3 Move()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        currentVel = Quaternion.AngleAxis(cameraCon.Yaw, Vector3.up) * input * MovementSpeed;

        Debug.DrawRay(transform.position, currentVel, Color.red);

        controller.Velocity = new Vector3(currentVel.x, controller.Velocity.y, currentVel.z);
        return input;
    }

    void Rotate()
    {
        //spelaren roterar åt den riktning den går mot
        //Vector3 targetDir = transform.forward - (-currentVel);

        //Spelaren roterar åt kamerans håll
        Vector3 targetDir = transform.forward - (-cameraCon.transform.forward);
        Vector3 modifiedDir = new Vector3(targetDir.x, 0, targetDir.z);

        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, modifiedDir, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }
}

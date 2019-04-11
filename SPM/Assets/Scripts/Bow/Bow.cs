using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    public GameObject parent;
    private Camera playerCamera;
    private float speed = 5;
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (speed < 45)
            {
                speed += speed * 2f * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            var direction = playerCamera.transform.forward;
            direction = Vector3.ProjectOnPlane(direction, Vector3.down);
            var arrow = Instantiate(Arrow, transform.position + new Vector3(0, 1, 0) + direction, Quaternion.LookRotation(direction * speed), parent.transform);
            arrow.GetComponent<Arrow>().ApplyInitialVelocity(direction.normalized * speed);
            speed = 5;
        }
    }

}

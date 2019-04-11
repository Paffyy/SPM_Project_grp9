using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    public GameObject Parent;
    public GameObject Player;
    public Camera playerCamera;
    private Vector3 bowOffset;
    private float speed = 7;
    private float angle = 0.1f;
    void Awake()
    {
        bowOffset = new Vector3(0.5f, 0, 0f);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (speed < 30)
            {
                speed += speed * 2f * Time.deltaTime;
            }
            if (angle < 0.3f)
            {
                angle += angle * Time.deltaTime;
            }
            Debug.Log(angle + ":" + speed);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            var direction = playerCamera.transform.forward;
            direction = Vector3.ProjectOnPlane(direction, Vector3.down);
            var arrow = Instantiate(Arrow, transform.position + direction, Quaternion.LookRotation(direction * speed), Parent.transform);
            arrow.GetComponent<Arrow>().ApplyInitialVelocity(direction.normalized * speed + new Vector3(0, speed / 90,0) * speed);
           
            speed = 5;
            angle = 0.1f;
        }
        UpdatePosition();
        UpdateRotation();
    }
    private void UpdateRotation(float swing = 0)
    {
        var direction = playerCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * bowOffset.normalized;
        transform.position = update * bowOffset.magnitude + Player.transform.position;
    }
}

using System;
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
    private ThirdPersonCrosshair thirdPersonCrosshair;
    private Vector3 bowOffset;
    private float speed = 10;
    private float angle = 0.1f;
    private float coolDownCounter = 0f;
    void Awake()
    {
        bowOffset = new Vector3(0.55f, 0.1f, 0f);
        thirdPersonCrosshair = GetComponent<ThirdPersonCrosshair>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownCounter <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (thirdPersonCrosshair != null)
                {
                    thirdPersonCrosshair.ToggleCrosshair(true);
                }
                if (speed < 25)
                {
                    speed += speed * Time.deltaTime;
                }
                if (angle < 0.4f)
                {
                    angle += angle * 2f * Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                ShootArrow();

                speed = 10;
                angle = 0.1f;
                coolDownCounter = 0.8f;
                if (thirdPersonCrosshair != null)
                {
                    thirdPersonCrosshair.ToggleCrosshair(false);
                }
            }
        }
        else
        {
            coolDownCounter -= Time.deltaTime;
        }
        if (thirdPersonCrosshair != null)
        {
            thirdPersonCrosshair.PositionCrosshair();
        }
        UpdatePosition();
        UpdateRotation();
    }

    private void ShootArrow()
    {
        var direction = playerCamera.transform.forward;
        direction = Vector3.ProjectOnPlane(direction, Vector3.down);
        var arrow = Instantiate(Arrow, transform.position + direction, Quaternion.LookRotation(direction * speed), Parent.transform);
        arrow.GetComponent<Arrow>().ApplyInitialVelocity(direction.normalized * speed + new Vector3(0, angle, 0) * speed);
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

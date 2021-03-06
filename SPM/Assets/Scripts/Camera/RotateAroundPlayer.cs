﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject RotateAroundObject;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Camera mainCamera;

    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }
    private void UpdateRotation(float swing = 0)
    {
        var direction = mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * offset.normalized;
        transform.position = update * offset.magnitude + RotateAroundObject.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    public GameObject parent;
    private Camera playerCamera;

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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            var arrow = Instantiate(Arrow, transform.position + new Vector3(0,1,0), Quaternion.Euler(playerCamera.transform.forward), parent.transform);
            var direction = playerCamera.transform.forward;
            direction = Vector3.ProjectOnPlane(direction, Vector3.down);
            arrow.GetComponent<Arrow>().ApplyInitialVelocity(direction.normalized * 35);
        }
    }

}

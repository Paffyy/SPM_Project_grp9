using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShit : MonoBehaviour
{
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.MovePosition(new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {

            Vector3 jump = Vector3.up * 5;
            controller.MovePosition(jump);
    }
}

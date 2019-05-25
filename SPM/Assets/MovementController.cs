using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public int speed;
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, 1 * speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0, -1 * speed) * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1 * speed, 0, 0) * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1 * speed, 0, 0) * Time.deltaTime;
        }
    }
}

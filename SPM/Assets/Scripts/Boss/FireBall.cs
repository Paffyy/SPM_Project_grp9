using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private CharacterController controller;
    public GameObject AOEEffect;
    //public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.IsGrounded())
        {
            GameObject obj = Instantiate(AOEEffect, transform.position, Quaternion.identity);
            //obj.transform.SetParent(Parent.transform);
            Destroy(gameObject);
        }
    }
}

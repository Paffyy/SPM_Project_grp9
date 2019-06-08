using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobController : MonoBehaviour {

    private float speed;
    private float direction;
    private Animator anim;
    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        direction = Input.GetAxis("Vertical");
        speed = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", speed);
        anim.SetFloat("Direction", direction);

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Sword");
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Jump");
        }
    }
}

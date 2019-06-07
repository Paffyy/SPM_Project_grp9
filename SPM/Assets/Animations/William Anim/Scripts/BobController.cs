using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobController : MonoBehaviour {

    private float Speed;
    private float Direction;
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Direction = Input.GetAxis("Vertical");
        Speed = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Speed);
        anim.SetFloat("Direction", Direction);

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

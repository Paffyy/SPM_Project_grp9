using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardShockwave : MonoBehaviour
{
    public float ForwardSpeed;
    public float AliveTime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = AliveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
            GameObject.Destroy(gameObject);
        else
        {
            gameObject.transform.position += gameObject.transform.forward * ForwardSpeed;
            timer -= Time.deltaTime;
        }
    }
}

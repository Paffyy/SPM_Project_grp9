using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardShockwave : MonoBehaviour
{
    public float ForwardSpeed;
    public float AliveTime;
    public int Damage;
    public float PushBack;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            Vector3 push = ((((player.transform.position) - (transform.position)).normalized* 3) + Vector3.up * 2.5f) * 5;
            player.TakeDamage(Damage, push);
        }
    }
}

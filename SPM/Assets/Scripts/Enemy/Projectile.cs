using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Velocity;
    public float Speed = 15.0f;
    public Player Player;
    // Start is called before the first frame update
    void Start()
    {
        Velocity = (Speed * Time.deltaTime) * (Player.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity;
    }
}

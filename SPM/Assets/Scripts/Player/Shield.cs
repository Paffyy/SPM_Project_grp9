using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private BoxCollider boxCollider;
    public Player Player;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("Träff");
            other.GetComponent<Projectile>().Velocity = -other.GetComponent<Projectile>().Velocity.normalized * other.GetComponent<Projectile>().Velocity.magnitude;
        }
    }

    //public void Reflect()
    //{
    //    Debug.Log("test");
    //    RaycastHit hit;
    //    if(Physics.BoxCast(transform.position, boxCollider.size, Player.Velocity.normalized, out hit, transform.rotation, Player.Velocity.magnitude * Time.deltaTime, Player.CollisionMask)){
    //        if (hit.collider.tag == "Projectile")
    //        {
    //            Debug.Log("test2");
    //            hit.collider.GetComponent<Projectile>().Velocity = hit.collider.GetComponent<Projectile>().Velocity.normalized * hit.collider.GetComponent<Projectile>().Velocity.magnitude;
    //        }
    //    }
    //}
}

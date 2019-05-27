﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Velocity;
    public float Speed = 15.0f;
    public GameObject Player;
    public GameObject ProjectileObject;
    public LayerMask ShieldLayer;
    public SphereCollider coll;
    public int Damage = 25;
    private bool isTerminating;
    private bool isReflected;
    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = Vector3.zero;
        isReflected = false;
        // Velocity = Speed  * (Player.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        //RaycastHit hit;
        //Debug.DrawLine(prevPos, transform.position);
        //if (prevPos != Vector3.zero)
        //{
        //    if (Physics.Linecast(prevPos, transform.position, out hit))
        //    {
        //        if (hit.collider.gameObject.CompareTag("Player"))
        //        {
        //            Debug.Log("PlayerHit");
        //        }
        //        else if (hit.collider.gameObject.CompareTag("Shield"))
        //        {
        //            isReflected = true;
        //            Velocity = Velocity.magnitude * Vector3.Reflect(Velocity.normalized, hit.normal);
        //            Debug.Log("ShieldHit");
        //        }
        //    }
        //}
        //prevPos = transform.position;
        //RaycastHit hit;
        //if (Physics.SphereCast(transform.position, coll.radius * 0.5f, Velocity.normalized, out hit, Velocity.magnitude * Time.deltaTime))
        //{
        //    if (hit.collider.gameObject.CompareTag("Shield"))
        //    {
        //        isReflected = true;
        //        Debug.Log(isReflected);
        //        Velocity = Velocity.magnitude * Vector3.Reflect(Velocity.normalized, hit.normal);
        //    }
        //    else if (hit.collider.gameObject.CompareTag("Player"))
        //    {
        //        Debug.Log("PlayerHit");
        //        hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
        //        Destroy(ProjectileObject);
        //    }
        //    else if (hit.collider.gameObject.CompareTag("Enemy") && isReflected)
        //    {
        //        Debug.Log("EnemyHit");
        //        hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage);
        //        Destroy(ProjectileObject);
        //    }
        //    else
        //    {
        //        if (!hit.collider.gameObject.CompareTag("Projectile"))
        //        {
        //            Debug.Log("OtherColl");
        //            Destroy(ProjectileObject);
        //        }
        //    }
        //}
    }

void Update()
    {
        //RaycastHit hit;
        //if (Physics.SphereCast(transform.position, coll.radius, Velocity.normalized, out hit, Velocity.magnitude * Time.deltaTime))
        //{
        //    if (hit.collider.gameObject.CompareTag("Shield"))
        //    {

        //        isReflected = true;
        //        Debug.Log(isReflected);
        //        Velocity = Velocity.magnitude * Vector3.Reflect(Velocity.normalized, hit.normal);
        //    }
        //    else if (hit.collider.gameObject.CompareTag("Player"))
        //    {
        //        Debug.Log("PlayerHit");
        //        hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
        //        Destroy(ProjectileObject);
        //    }
        //    else if (hit.collider.gameObject.CompareTag("Enemy") && isReflected)
        //    {
        //        Debug.Log("EnemyHit");
        //        hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage);
        //        Destroy(ProjectileObject);
        //    }
        //    else
        //    {
        //        if (!hit.collider.gameObject.CompareTag("Projectile"))
        //        {
        //            Debug.Log("OtherColl");
        //            Destroy(ProjectileObject);
        //        }
        //    }
        //}
        transform.position += Velocity * Time.deltaTime;
        if (!isTerminating)
        {
            StartCoroutine(Terminate());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isReflected)
        {
            Vector3 pushBack = Vector3.ProjectOnPlane(Velocity.normalized, Vector3.up) * 2 + (Vector3.up * 2) * 3;
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage,pushBack,transform.position);
            Destroy(ProjectileObject);
        } else if (other.gameObject.CompareTag("Shield"))
        {
            isReflected = true;
            Velocity = -Velocity;
            transform.rotation = Quaternion.LookRotation(other.gameObject.transform.forward);

           // other.gameObject.GetComponentInParent<Shield>().TakeDamage(Damage);
        } else if(other.gameObject.CompareTag("Enemy") && isReflected)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage * 2);
            Destroy(ProjectileObject);
        } else
        {
            //if (!other.gameObject.CompareTag("Projectile"))
            //{
            //    Debug.Log("OtherColl");
            //    Destroy(ProjectileObject);
            //}
        }
    }

    IEnumerator Terminate()
    {
        isTerminating = true;
        yield return new WaitForSeconds(5);
        Destroy(ProjectileObject);
    }
}

using System.Collections;
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

    void Start()
    {
        prevPos = Vector3.zero;
        isReflected = false;
    }


    void Update()
    {
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
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage, pushBack, transform.position);
            Destroy(ProjectileObject);
        }
        else if (other.gameObject.CompareTag("Shield"))
        {
            isReflected = true;
            Velocity = -Velocity;
            transform.rotation = Quaternion.LookRotation(-transform.forward);
        }
        else if (other.gameObject.CompareTag("Enemy") && isReflected)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage * 4);
            Destroy(ProjectileObject);
        }
        else
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

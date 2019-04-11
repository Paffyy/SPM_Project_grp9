using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Velocity;
    public float Speed = 15.0f;
    public GameObject Player;
    Vector3 prev;
    private bool isTerminating;
    public GameObject ProjectileObject;
    public LayerMask ShieldLayer;
    public SphereCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        prev = Vector3.zero;
       // Velocity = Speed  * (Player.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, coll.radius * 0.2f, Velocity.normalized, out hit, Velocity.magnitude * Time.deltaTime, ShieldLayer))
        {
            if (hit.collider.gameObject.CompareTag("Shield"))
            {
                Debug.Log("Collided");
                Velocity = Velocity.magnitude * Vector3.Reflect(Velocity.normalized, hit.normal);
            }
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, coll.radius * 0.2f, Velocity.normalized, out hit, Velocity.magnitude * Time.deltaTime, ShieldLayer))
        {
            if (hit.collider.gameObject.CompareTag("Shield"))
            {

                Debug.Log("Collided");
                Velocity = Velocity.magnitude * Vector3.Reflect(Velocity.normalized, hit.normal);
            }
        }
        prev = transform.position;
        transform.position += Velocity * Time.deltaTime;
        if (!isTerminating)
        {
            StartCoroutine(Terminate());
        }
    }

    IEnumerator Terminate()
    {
        isTerminating = true;
        yield return new WaitForSeconds(5);
        Destroy(ProjectileObject);
    }
}

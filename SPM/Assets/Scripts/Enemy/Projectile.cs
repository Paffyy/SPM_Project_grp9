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

    // Start is called before the first frame update
    void Start()
    {
        prev = Vector3.zero;
       // Velocity = Speed  * (Player.transform.position - transform.position).normalized;
    }

    //void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Linecast(prev, transform.position, out hit))
    //    {
    //        if (hit.collider.gameObject.CompareTag("Shield"))
    //        {
    //            Debug.Log("Collided");
    //            Velocity = -Velocity;
    //        }
    //    }
    //    prev = transform.position;
    //    transform.position += Velocity * Time.deltaTime;
    //}

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Linecast(prev, transform.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Shield"))
            {
                Debug.Log("Collided");
                Velocity = -Velocity;
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

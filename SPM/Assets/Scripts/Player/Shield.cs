using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private BoxCollider boxCollider;
    public Player Player;
    public LayerMask ProjectileMask;
    public Camera playerCamera;
    private Vector3 shieldPos;
    // Start is called before the first frame update
    void Start()
    {
        shieldPos = new Vector3(0f, 0.2f, 1.0f);
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = playerCamera.transform.forward; 
        //direction = Vector3.ProjectOnPlane(direction * 3.0f, new Vector3(0,-0.5f,0));
        transform.rotation = Quaternion.LookRotation(direction);
        Vector3 update = transform.rotation * shieldPos.normalized;
        transform.position = update * shieldPos.magnitude + Player.transform.position;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Projectile"))
    //    {
    //        Debug.Log("Träff");
    //        other.GetComponent<Projectile>().Velocity = -other.GetComponent<Projectile>().Velocity.normalized * other.GetComponent<Projectile>().Velocity.magnitude;
    //    }
    //}

    public void Reflect()
    {
        Debug.Log("test");
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, boxCollider.size / 2, transform.position.normalized, out hit, transform.rotation, transform.position.magnitude * Time.deltaTime, ProjectileMask))
        {
            if (hit.collider != null)
            {
                Debug.Log("test2");
                hit.collider.GetComponent<Projectile>().Velocity = -hit.collider.GetComponent<Projectile>().Velocity;
            }
        }
    }
}

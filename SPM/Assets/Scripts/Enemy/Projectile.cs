using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Velocity { get; set; }
    public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage = 8;
    private bool isTerminating;
    private bool isReflected;
    private Vector3 prevPos;
    [SerializeField]
    private AudioClip soundClip;
 
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
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, pushBack, transform.position);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Shield"))
        {
            isReflected = true;
            Velocity = -Velocity;
            transform.rotation = Quaternion.LookRotation(-transform.forward);
            AudioEventInfo audioEvent = new AudioEventInfo(soundClip);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ShieldBlock, audioEvent);
        }
        else if (other.gameObject.CompareTag("Enemy") && isReflected)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage * 4);
            Destroy(gameObject);
        }
    }

    IEnumerator Terminate()
    {
        isTerminating = true;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    public float LifeTime { get; set; }
    public bool LifeTimeBool { get; set; }
    public SphereCollider ContactCollider { get { return contactCollider; } }
    [SerializeField]
    private SphereCollider contactCollider;
    [SerializeField]
    private LayerMask collisionMask;
    private int damage = 10;
    private float timer;

    void Start()
    {
        StartCoroutine(InflictDamage());
        timer = LifeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (LifeTimeBool)
        {
            if (timer < 0)
                Destroy(gameObject);
        }
    }

    private GameObject CheckArea()
    {
        List<Collider> colliders = Manager.Instance.GetAoeHit(transform.position, collisionMask, ContactCollider.radius * ((transform.localScale.x + transform.localScale.z) / 2));
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Player"))
            {
                return c.gameObject;
            }
        }
        return null;
    }

    IEnumerator InflictDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject contactObject = CheckArea();
            if (contactObject != null)
            {
                Vector3 direction = contactObject.transform.position - transform.position;
                Vector3 pushBack = Vector3.ProjectOnPlane(direction, Vector3.up) * 2 + (Vector3.up * 2) * 3;
                contactObject.GetComponent<PlayerHealth>().TakeDamage(damage,pushBack,transform.position);
            }
        }
    }

}

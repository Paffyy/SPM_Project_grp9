using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{

    public SphereCollider SphereCollider;
    public LayerMask CollisionMask;
    public int Damage = 10;
    //public GameObject Player;
    public bool LifeTimeBool = false;
    public float LifeTime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InflictDamage());
        timer = LifeTime;
    }

    // Update is called once per frame
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
        List<Collider> colliders = Manager.Instance.GetAoeHit(transform.position, CollisionMask, SphereCollider.radius * ((transform.localScale.x + transform.localScale.z) / 2));
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
            GameObject obj = CheckArea();
            if (obj != null)
            {
                //Player.GetComponent<PlayerHealth>().TakeDamage(Damage);
                obj.GetComponent<PlayerHealth>().TakeDamage(Damage);
            }
        }
    }

}

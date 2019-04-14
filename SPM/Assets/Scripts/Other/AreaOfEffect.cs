using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{

    public SphereCollider SphereCollider;
    public LayerMask CollisionMask;
    public int Damage = 10;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InflictDamage());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private bool CheckArea()
    {
        List<Collider> colliders = Manager.Instance.GetAoeHit(transform.position, CollisionMask, SphereCollider.radius * ((transform.localScale.x + transform.localScale.z) / 2));
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator InflictDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            if (CheckArea())
            {
                Player.GetComponent<PlayerHealth>().TakeDamage(Damage);
            }
        }
    }

}

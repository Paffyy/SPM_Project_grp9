using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAttack : MonoBehaviour
{
    private Boss boss;

    void Start()
    {
        boss = GetComponentInParent<Boss>();
    }


    public void OnTriggerEnter(Collider collider)
    {

            if(collider.gameObject.tag == "Player")
            {
                PlayerHealth player = collider.GetComponent<PlayerHealth>();
                player.TakeDamage(boss.Damage, ((player.transform.position - boss.transform.position).normalized * 5) + (Vector3.up * 2) * 7, transform.position);
            }
    }
}

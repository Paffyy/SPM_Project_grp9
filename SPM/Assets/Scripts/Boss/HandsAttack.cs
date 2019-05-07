using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAttack : MonoBehaviour
{
    private bool isActive = false;
    private Boss boss;
    //private float timeActive;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
                isActive = false;
        }
    }

    public void ActivateHand(float stateTime)
    {

        isActive = true;
        timer = stateTime;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (isActive)
        {
            if(collider.gameObject.tag == "Player")
            {
                PlayerHealth player = collider.GetComponent<PlayerHealth>();
                player.TakeDamage(boss.Damage, ((player.transform.position - boss.transform.position).normalized * 5) + (Vector3.up * 2) * 7, transform.position);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAttack : MonoBehaviour
{
    private bool isActive = false;
    private Boss boss;
    private float timeActive;
    private float timer;
    public AnimationClip animClip;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponentInParent<Boss>();
        timeActive = animClip.length;
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

    public void ActivateHand()
    {
        isActive = true;
        timer = timeActive;
    }

    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("hit" + isActive + timer + timeActive);
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

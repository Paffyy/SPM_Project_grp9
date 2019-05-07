using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEffect : MonoBehaviour
{
    public float lifetime;
    private float timer;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        timer = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            Destroy(gameObject);
    }

    public void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.GetComponent<PlayerHealth>().TakeDamage(Damage, Vector3.up * 2, Vector3.zero);
        }
    }

}

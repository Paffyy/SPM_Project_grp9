using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{

    private Collider Trigger;
    public GameObject BossCanvas;
    public Boss Boss;
    private EnemyHealth health;
    // Start is called before the first frame update

    private void Awake()
    {
        Trigger = GetComponent<Collider>();
        Trigger.isTrigger = true;
        health = Boss.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            BossCanvas.SetActive(true);
            health.SetupHealthSlider();
            Debug.Log("Fight Start!");
        }
    }
}

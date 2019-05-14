using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackAnim : MonoBehaviour
{
    public Animator Anim;
    public ParticleSystem Trails;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)){
            Anim.SetBool("Attack", true);
        }
    }

    public void GoToIdle()
    {
        Anim.SetBool("Attack", false);
    }

    public void SpawnTrail()
    {
        Trails.Play();
        Debug.Log("Start trails");
    }

    public void StopTrail()
    {
        Trails.Stop();
        Debug.Log("Stop trails");
    }
}

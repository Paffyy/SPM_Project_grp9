using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseEnemy
{
    public GameObject FireArea;
    [HideInInspector]public Animator anim;
    public GameObject FireEffectOnBoss;
    public GameObject ShockWaveObject;
    public float ShockWaveObejctYOffset;

    protected override void Awake()
    {
        base.Awake();
        FireEffectOnBoss.SetActive(false);
        anim = GetComponent<Animator>();
    }


    public void SpawnShockWaveInState()
    {
        Debug.Log("spawn");
        //Y-värdet är beroende på offseten på showwave
        GameObject.Instantiate(ShockWaveObject, new Vector3(transform.position.x, ShockWaveObejctYOffset, transform.position.z),
        transform.rotation);
    }

}

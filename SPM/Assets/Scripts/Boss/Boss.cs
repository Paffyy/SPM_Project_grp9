using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseEnemy
{
    public GameObject FireArea;
    [HideInInspector]public Animator anim;
    public GameObject FireEffectOnBoss;

    protected override void Awake()
    {
        base.Awake();
        FireEffectOnBoss.SetActive(false);
        anim = GetComponent<Animator>();
    }

}

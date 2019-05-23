using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [HideInInspector]public Animator anim;
    [HideInInspector]public GameObject firesOfHeavenContainer;
    protected override void Awake()
    {
        base.Awake();
        firesOfHeavenContainer = new GameObject("fires");
        anim = GetComponent<Animator>();
    }
}

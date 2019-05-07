using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseEnemy
{
    public GameObject FireArea;
    [HideInInspector]public Animator anim;
    public HandsAttack LeftHand;
    public HandsAttack RightHand;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBaseEnemy : BaseEnemy
{
    [HideInInspector]
    public Animator Anim;
    [HideInInspector]
    public GameObject FireBall;

    public GameObject RightHand { get { return rightHand; } set { rightHand = value; } }
    public GameObject LeftHand { get { return leftHand; }set { leftHand = value; } }

    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject leftHand;


    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponent<Animator>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBaseEnemy : BaseEnemy
{
    public Animator Anim { get { return animator; } }
    public GameObject RightHand { get { return rightHand; } set { rightHand = value; } }
    public GameObject LeftHand { get { return leftHand; }set { leftHand = value; } }

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject fireBall;


    protected override void Awake()
    {
        base.Awake();
       // Anim = GetComponent<Animator>();
    }

    public void InstantiateFireBall()
    {
        GameObject fireBallClone;
        if (Anim.GetBool("IsUsingRightHand"))
        {
            fireBallClone = Instantiate(fireBall, RightHand.transform.position, transform.rotation);
            fireBallClone.GetComponent<Projectile>().Velocity = fireBallClone.GetComponent<Projectile>().Speed * (player.transform.position - fireBallClone.transform.position).normalized;
        }
        else if(!Anim.GetBool("IsUsingRightHand"))
        {
            fireBallClone = Instantiate(fireBall, LeftHand.transform.position, transform.rotation);
            fireBallClone.GetComponent<Projectile>().Velocity = fireBallClone.GetComponent<Projectile>().Speed * (player.transform.position - fireBallClone.transform.position).normalized;
        }
    }
}

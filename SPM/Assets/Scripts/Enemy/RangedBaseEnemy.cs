using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBaseEnemy : BaseEnemy
{
    public Animator Anim { get { return animator; } }
    public GameObject RightHand { get { return rightHand; } set { rightHand = value; } }
    public GameObject LeftHand { get { return leftHand; } set { leftHand = value; } }

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    private AudioClip attackClip;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void InstantiateFireBall()
    {
        GameObject fireBallClone;
        if (Anim.GetBool("IsUsingRightHand"))
        {
            fireBallClone = Instantiate(fireBall, RightHand.transform.position, transform.rotation);
            fireBallClone.GetComponent<Projectile>().Velocity = fireBallClone.GetComponent<Projectile>().Speed * ((player.transform.position + new Vector3(0, 1.2f, 0)) - fireBallClone.transform.position).normalized;
            PlaySound();
        }
        else if(!Anim.GetBool("IsUsingRightHand"))
        {
            fireBallClone = Instantiate(fireBall, LeftHand.transform.position, transform.rotation);
            fireBallClone.GetComponent<Projectile>().Velocity = fireBallClone.GetComponent<Projectile>().Speed * ((player.transform.position + new Vector3(0, 1.2f, 0)) - fireBallClone.transform.position).normalized;
            PlaySound();
        }
    }

    private void PlaySound()
    {
        AudioEventInfo audioEventInfo = new AudioEventInfo(attackClip, audioSource);
        EventHandler.Instance.FireEvent(EventHandler.EventType.EnemyAttackEvent, audioEventInfo);
    }
}

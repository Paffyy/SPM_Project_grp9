﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{

    public GameObject BossCanvas;
    public Boss Boss;
    private EnemyHealth health;
    public GameObject FightBorder;
    // Start is called before the first frame update

    public void Start()
    {
        Register();
    }

    private void Awake()
    {

        health = Boss.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.BossFightTrigger, TriggerFight);
    }

    public void TriggerFight(BaseEventInfo e)
    {
        health.SetupHealthSlider();
        BossCanvas.SetActive(true);
        FightBorder.SetActive(true);
        Boss.Transition<BossAttackState>();
    }

}

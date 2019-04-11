using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : StateMachine
{
    public GameObject Shield;
    public GameObject Bow;
    public GameObject Sword;
    public Button ShieldIcon;
    public Button BowIcon;
    public Button SwordIcon;

    protected override void Awake()
    {
        base.Awake();
    }
}

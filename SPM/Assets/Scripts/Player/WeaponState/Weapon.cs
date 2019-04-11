using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : StateMachine
{
    public GameObject Shield;
    public GameObject Bow;
    public GameObject Sword;
    protected override void Awake()
    {
        base.Awake();
    }
}

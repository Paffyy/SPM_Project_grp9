using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : StateMachine
{
    public GameObject Shield;
    public GameObject Bow;
    protected override void Awake()
    {
        base.Awake();
    }
}

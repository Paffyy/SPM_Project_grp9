using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : StateMachine
{
    public GameObject Shield;
    public GameObject Bow;
    public GameObject BowFirstPerson;
    public GameObject Sword;
    public Image ShieldIcon;
    public Image BowIcon;
    public Image SwordIcon;
    public int ArrowCount { get { return arrowCount; } set { arrowCount = value; } }

    [SerializeField]
    private int arrowCount;

    [HideInInspector]
    public Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }
}

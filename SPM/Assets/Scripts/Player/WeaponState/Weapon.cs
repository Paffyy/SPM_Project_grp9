using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : StateMachine
{

    public int CurrentStateID
    {
        get { return currentStateID; }
        set { currentStateID = value; }
    }
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

    private int currentStateID;

    [HideInInspector]
    public Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    public void ChangeState(int stateID)
    {
        switch (stateID)
        {
            case 0:
                Transition<SwordState>();
                break;
            case 1:
                Transition<BowAimState>();
                break;
        }
    }
}

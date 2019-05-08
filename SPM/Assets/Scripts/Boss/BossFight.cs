using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

        EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, OnBossDeath);
    }

    public void OnBossDeath(BaseEventInfo e)
    {
        DeathEventInfo death = (DeathEventInfo)e;

        if(death.GameObject.name == "Boss")
        {

            //borde göra något fadeout av ui:n
            BossCanvas.SetActive(false);
            FightBorder.SetActive(false);

            //temp
            SceneManager.LoadScene(0);

        }
    }

}

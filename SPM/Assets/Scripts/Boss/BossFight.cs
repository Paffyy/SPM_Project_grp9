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
    [SerializeField]
    private GameObject endScreen;

    private bool hasPlayedRevSound;

    public void Start()
    {
        hasPlayedRevSound = false;
        Register();
    }

    private void Awake()
    {

        health = Boss.GetComponent<EnemyHealth>();
    }

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
        health.ActivateHealthBar();
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
            endScreen.SetActive(true);
            if (!hasPlayedRevSound)
            {
                RevSoundEventInfo revSoundEventInfo = new RevSoundEventInfo();
                EventHandler.Instance.FireEvent(EventHandler.EventType.RevAudioEvent, revSoundEventInfo);
                hasPlayedRevSound = true;
                Invoke("LoadMainMenu", 5f);
            }
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}

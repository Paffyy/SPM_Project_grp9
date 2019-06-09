using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownManager : MonoBehaviour
{
    public bool BladeStormOnCoolDown { get; set; }
    public bool ArrowRainOnCoolDown { get; set; }
    public bool RangedEnemyAttackOnCoolDown { get; set; }
    [SerializeField]
    private Image bladeStormCoolDownImage;
    [SerializeField]
    private Image arrowRainCoolDownImage;
    private float bladeStormCoolDownTimer;
    private float arrowRainCoolDownTimer;
    private float bladeStormCoolDown;
    private float arrowRainCoolDown;
    private static CoolDownManager instance;

    public static CoolDownManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CoolDownManager>();
            return instance;
        }
    }

    void Update()
    {
        if (BladeStormOnCoolDown)
        {
            bladeStormCoolDownTimer -= Time.deltaTime;
            bladeStormCoolDownImage.fillAmount += 1 / bladeStormCoolDown * Time.deltaTime;
            if (bladeStormCoolDownTimer <= 0)
            {
                bladeStormCoolDownImage.fillAmount = 0;
                BladeStormOnCoolDown = false;
            }
        }
        if (ArrowRainOnCoolDown)
        {
            arrowRainCoolDownTimer -= Time.deltaTime;
            arrowRainCoolDownImage.fillAmount += 1 / arrowRainCoolDown * Time.deltaTime;
            if (arrowRainCoolDownTimer <= 0)
            {
                arrowRainCoolDownImage.fillAmount = 0;
                ArrowRainOnCoolDown = false;
            }
        }
    }
    
   public void StartBladeStormCoolDown(float coolDown)
    {
        bladeStormCoolDown = coolDown;
        bladeStormCoolDownTimer = coolDown;
        BladeStormOnCoolDown = true;
    }

    public void StartArrowRainCoolDown(float coolDown)
    {
        arrowRainCoolDown = coolDown;
        arrowRainCoolDownTimer = coolDown;
        ArrowRainOnCoolDown = true;
    }

}

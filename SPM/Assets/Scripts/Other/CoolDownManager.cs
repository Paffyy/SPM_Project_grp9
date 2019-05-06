using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownManager : MonoBehaviour
{
    public bool BladeStormOnCoolDown = false;
    public bool ArrowRainOnCoolDown = false;
    public Image BladeStormCoolDownImage;
    public Image ArrowRainCoolDownImage;
    //public Text BladeStormCoolDownText;
    //public Text ArrowRainCoolDownText;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BladeStormOnCoolDown)
        {
            bladeStormCoolDownTimer -= Time.deltaTime;
            BladeStormCoolDownImage.fillAmount += 1 / bladeStormCoolDown * Time.deltaTime;
            if (bladeStormCoolDownTimer <= 0)
            {
                BladeStormCoolDownImage.fillAmount = 0;
                BladeStormOnCoolDown = false;
            }
        }
        if (ArrowRainOnCoolDown)
        {
            arrowRainCoolDownTimer -= Time.deltaTime;
            ArrowRainCoolDownImage.fillAmount += 1 / arrowRainCoolDown * Time.deltaTime;
            if (arrowRainCoolDownTimer <= 0)
            {
                ArrowRainCoolDownImage.fillAmount = 0;
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

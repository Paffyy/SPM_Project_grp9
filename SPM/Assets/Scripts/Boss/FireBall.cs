using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    //public float SplashDamageArea;
    //public float SplashDamage;

    private CharacterController controller;
    public AreaOfEffect AOEEffect;
    public float LifeTimeOFFireEffect;
    private PlayerHealth player;
    //public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        AOEEffect.LifeTimeBool = true;
        AOEEffect.LifeTime = LifeTimeOFFireEffect;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.IsGrounded())
        {
            GameObject obj = Instantiate(AOEEffect.gameObject, transform.position, Quaternion.identity);

            //obj.transform.SetParent(Parent.transform);
            Destroy(gameObject);
        }
    }
}

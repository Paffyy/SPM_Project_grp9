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
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private int impactDamage;
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
            var collidersHit = Manager.Instance.GetAoeHit(transform.position, playerLayer, AOEEffect.SphereCollider.radius * ((AOEEffect.transform.localScale.x + AOEEffect.transform.localScale.z) / 2));
            if (collidersHit.Count > 0)
            {
                foreach (var item in collidersHit)
                {
                   var playerHealth =  item.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(impactDamage);
                    }
                }
            }
            GameObject obj = Instantiate(AOEEffect.gameObject, transform.position, Quaternion.identity);

            //obj.transform.SetParent(Parent.transform);
            Destroy(gameObject);
        }
    }
}

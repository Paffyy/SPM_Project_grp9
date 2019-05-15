using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    public float Radius;
    [Range(0,360)]
    public float Angle;
    public LayerMask CollisionMask;
    public float CoolDownValue;
    public GameObject PlayerObject;
    public Camera playerCamera;
    public int Damage = 50;
    public int BladeStormDamage = 5;
    public GameObject BladeStormEffect;
    public bool IsBladeStorming;
    private float coolDownCounter;
    private bool onCooldown;
    private Vector3 swordOffset;
    private float swingValue = 70f;
    private float bladeStormCoolDown = 10.0f;
    private float BladeStormTimer = 3f;
    public Animator Anim;
    public ParticleSystem Trails;

    void Start()
    {
        swordOffset = new Vector3(0.5f, 0.2f, 0.55f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !CoolDownManager.Instance.BladeStormOnCoolDown && !IsBladeStorming)
        {
            BladeStorm();
            IsBladeStorming = true;
            CoolDownManager.Instance.StartBladeStormCoolDown(bladeStormCoolDown);
        }
        if (IsBladeStorming)
        {
            if (BladeStormEffect.activeInHierarchy == false)
            {
                BladeStormEffect.SetActive(true);
                BladeStormEffect.transform.position = PlayerObject.transform.position;
            }
            var direction = playerCamera.transform.forward;
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90 + swingValue, 0, 0);
            BladeStormTimer -= Time.deltaTime;
            if (BladeStormTimer <= 0)
            {
                BladeStormEffect.SetActive(false);
                Debug.Log("Stop BS!");
                IsBladeStorming = false;
                BladeStormTimer = 3f;
                StopAllCoroutines();
            }
        }
        if(!IsBladeStorming)
        {
            if (coolDownCounter < 0)
            {

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    
                    coolDownCounter = CoolDownValue;
                    Attack();
                }
                UpdateRotation();
            }
            else
            {
                coolDownCounter -= Time.deltaTime;

                if (coolDownCounter < CoolDownValue)
                {
                    UpdateRotation(swingValue);
                }
                else
                {
                    UpdateRotation(swingValue);
                }
            }
        }
        UpdatePosition();
    }
    IEnumerator InflictBladeStormDamage()
    { 
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            List<Collider> colliders = Manager.Instance.GetAoeHit(PlayerObject.transform.position, CollisionMask, 3.5f);
           // Gizmos.DrawSphere(PlayerObject.transform.position, BladeStormCollider.radius * ((transform.localScale.x + transform.localScale.z) / 2));
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Enemy"))
                {
                    c.gameObject.GetComponent<EnemyHealth>().TakeDamage(BladeStormDamage, true);
                }
            }
        }
    }

    public void GoToIdle()
    {
        Anim.SetBool("Attack", false);
    }

    public void SpawnTrail()
    {
        var em = Trails.emission;
        em.enabled = true;
        Debug.Log("Start trails");
    }

    public void StopTrail()
    {
        var em = Trails.emission;
        em.enabled = false;
        Debug.Log("Stop trails");
    }

    private void BladeStorm()
    {
        StartCoroutine(InflictBladeStormDamage());
    }

    private void UpdateRotation(float swing = 0)
    {
        var direction = playerCamera.transform.forward;
      //  transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90 + swing, 0, 0);
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-15, 90, 0);
    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * swordOffset.normalized;
        transform.position = update * swordOffset.magnitude + PlayerObject.transform.position;
    }

    void Attack()
    {
        Anim.SetBool("Attack", true);
    }

    void CheckCollision()
    {
        var enemyInRange = Manager.Instance.GetFrontConeHit(playerCamera.transform.forward, PlayerObject.transform, CollisionMask, Radius, Angle);
        foreach (var item in enemyInRange)
        {
            DealDamage(item);
        }
    }

    private void DealDamage(Collider item)
    {
        //testing
        Vector3 pushBack = ((item.gameObject.transform.position - PlayerObject.transform.position).normalized + Vector3.up * 4) * 5;
        item.gameObject.GetComponent<Health>().TakeDamage(Damage, pushBack, PlayerObject.transform.position);

        //item.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage);

        Color c = item.GetComponent<Renderer>().material.color;
        item.GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(RemoveRedColor(item, c));
    }
    //testing
    IEnumerator RemoveRedColor(Collider item, Color c)
    {
        yield return new WaitForSeconds(0.2f);
        if(item != null)
             item.GetComponent<Renderer>().material.color = c;
    }
}

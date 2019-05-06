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
    public int BladeStormDamage = 20;
    public GameObject BladeStormEffect;
    private float coolDownCounter;
    private bool onCooldown;
    private Vector3 swordOffset;
    private float swingValue = 70f;
    //private bool bladeStormOnCoolDown;
    private float bladeStormCoolDown = 10.0f;
    public bool IsBladeStorming;
    private float BladeStormTimer = 3f;

 

    void Start()
    {
        swordOffset = new Vector3(0.5f, 0, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !CoolDownManager.Instance.BladeStormOnCoolDown && !IsBladeStorming)
        {
            BladeStorm();
            IsBladeStorming = true;
            CoolDownManager.Instance.StartBladeStormCoolDown(bladeStormCoolDown);
          //  bladeStormOnCoolDown = true;
        }
        //if (bladeStormOnCoolDown && !isBladeStorming)
        //{
        //    bladeStormCoolDown -= Time.deltaTime;
        //}
        //if(bladeStormCoolDown <= 0)
        //{
        //    bladeStormOnCoolDown = false;
        //    bladeStormCoolDown = 10.0f;

        //}
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
                    CheckCollision();
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
            yield return new WaitForSeconds(1f);
            List<Collider> colliders = Manager.Instance.GetAoeHit(PlayerObject.transform.position, CollisionMask, 3.5f);
           // Gizmos.DrawSphere(PlayerObject.transform.position, BladeStormCollider.radius * ((transform.localScale.x + transform.localScale.z) / 2));
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Hit");
                    c.gameObject.GetComponent<EnemyHealth>().TakeDamage(BladeStormDamage);
                }
            }
        }
    }

    private void BladeStorm()
    {
        StartCoroutine(InflictBladeStormDamage());
    }

    private void UpdateRotation(float swing = 0)
    {
        var direction = playerCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90 + swing, 0, 0);
    }

    private void UpdatePosition()
    {
        Vector3 update = transform.rotation * swordOffset.normalized;
        transform.position = update * swordOffset.magnitude + PlayerObject.transform.position;
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
        item.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage);
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

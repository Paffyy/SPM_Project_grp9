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
    private float coolDownCounter;
    private bool onCooldown;
    private Vector3 swordOffset;
    private float swingValue = 70f;

    void Start()
    {
        swordOffset = new Vector3(0.5f, 0, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownCounter < 0)
        {

            if (Input.GetKeyDown(KeyCode.Mouse1))
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
        UpdatePosition();
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
        item.GetComponent<Renderer>().material.color = c;
    }
}

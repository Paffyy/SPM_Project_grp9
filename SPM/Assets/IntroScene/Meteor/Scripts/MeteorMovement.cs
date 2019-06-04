using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject impactPrefab;
    [SerializeField]
    private List<GameObject> trails;
    private Rigidbody rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (speed != 0 && rigidBody != null)
        {
            rigidBody.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        if (!collision.gameObject.CompareTag("MeteorGround"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<BoxCollider>());
        }
        else
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            if (impactPrefab != null)
            {
                if (!IntroScene.IntroSceneInstance.FirstMeteorHit)
                {
                    IntroScene.IntroSceneInstance.ActivateCameraWalk();
                    IntroScene.IntroSceneInstance.FirstMeteorHit = true;
                }
                GameObject impactVFX = Instantiate(impactPrefab, pos, rot) as GameObject;
                impactVFX.GetComponent<AudioSource>().Play();
                Destroy(impactVFX, 60f);
            }

            if(trails.Count > 0)
            {
                foreach(GameObject trail in trails)
                {
                    trail.transform.parent = null;
                    ParticleSystem ps = trail.GetComponent<ParticleSystem>();
                    if(ps != null)
                    {
                        ps.Stop();
                        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{

    private AudioSource source;
    [SerializeField]
    private AudioClip clip;


    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        source.loop = true;
        source.time = Random.Range(0, clip.length);
        source.Play();
    }
}

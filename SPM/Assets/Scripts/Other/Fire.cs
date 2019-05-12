using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem[] ParticleSystems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutOut()
    {
        foreach (var ps in ParticleSystems)
        {
            var main = ps.main;
            main.loop = false;
        }
    }
}

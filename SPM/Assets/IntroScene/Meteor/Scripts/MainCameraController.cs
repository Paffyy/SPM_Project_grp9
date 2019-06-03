using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Animator Anim { get { return anim; } }

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        
    }

    private void GoToLookUpState()
    {
        IntroScene.IntroSceneInstance.ActivateCameraLookUp();
    }

    private void GoToLookAroundState()
    {
        IntroScene.IntroSceneInstance.ActivateCameraLookAround();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meteor"))
        {
            IntroScene.IntroSceneInstance.ActivateBlackScreen();
        }
    }
}

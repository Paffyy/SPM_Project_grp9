using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    public static IntroScene IntroSceneInstance;
    public bool FirstMeteorHit { get; set; }

    [SerializeField]
    private GameObject blackScreen;
    [SerializeField]
    private GameObject meteorGround;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private List<GameObject> meteors;
    [SerializeField]
    private GameObject lastMeteor;

    void Start()
    {
        IntroSceneInstance = this;
        FirstMeteorHit = false;
        foreach(GameObject zone in GameController.GameControllerInstance.Zones.Values)
        {
            zone.GetComponent<RevitalizeZone>().RevitalizeTheZoneInstant();
        }
        mainCamera.enabled = true;
    }

    void Update()
    {

    }

    private void ActivateMultipleMeteors()
    {
        foreach(GameObject meteor in meteors)
        {
            meteor.SetActive(true);
        }
    }

    public void ActivateCameraLookUp()
    {
        lastMeteor.SetActive(true);
        mainCamera.GetComponent<MainCameraController>().Anim.SetBool("MeteorIsComingFromAbove", true);
    }


    public void ActivateCameraLookAround()
    {
        ActivateMultipleMeteors();
        mainCamera.GetComponent<MainCameraController>().Anim.SetBool("MultipleMeteorsIsIncoming", true);
    }

    public void ActivateCameraWalk()
    {
        mainCamera.GetComponent<MainCameraController>().Anim.SetBool("MeteorHasLanded", true);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void ActivateBlackScreen()
    {
        blackScreen.SetActive(true);
        Invoke("LoadFirstLevel", 1f);
    }
}

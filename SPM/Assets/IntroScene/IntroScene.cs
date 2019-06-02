using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    void Start()
    {
        foreach(GameObject zone in GameController.GameControllerInstance.Zones.Values)
        {
            zone.GetComponent<RevitalizeZone>().RevitalizeTheZoneInstant();
        }
    }

    void Update()
    {
        
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}

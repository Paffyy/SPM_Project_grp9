using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
   
    public static bool IsPaused;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject UIHud;


    void Update()
    {
        if (Input.GetKeyDown(KeybindManager.Instance.PauseMenuButton.GetKeyCode()))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        UIHud.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
        Manager.Instance.IsPaused = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        UIHud.SetActive(false);
        Time.timeScale = 0f;
        IsPaused = true;
        Manager.Instance.IsPaused = true;
    }
}

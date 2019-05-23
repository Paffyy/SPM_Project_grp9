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


    void Start()
    {
        Register();
    }
    void Update()
    {
        if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.PauseMenuButton, InputManager.ControllMode.AllStates))
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

    void Register()
    {
        Debug.Log("register");
        EventHandler.Instance.Register(EventHandler.EventType.UIButtonEvent, CheckButtonClick);
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

    private void CheckButtonClick(BaseEventInfo e)
    {
        var buttonEventInfo = e as UIButtonEventInfo;
        if (buttonEventInfo != null)
        {
            Debug.Log("boop ius " + buttonEventInfo.MenuEnum.ToString());
            //if (buttonEventInfo.MenuEnum != UIButtonEventInfo.Menu.MainMenu)
            //{
                Debug.Log("boop " + (int)buttonEventInfo.ButtonEnum);
                switch ((int)buttonEventInfo.ButtonEnum)
                {
                    case 0:
                        ResumeGameButton();
                        break;
                    case 1:
                        OptionsButton();
                        break;
                    case 2:
                        SaveAndQuitButton();
                        break;

                }
            //}

        }
    }


    private void ResumeGameButton()
    {

        Resume();
    }

    private void OptionsButton()
    {

    }

    private void SaveAndQuitButton()
    {

    }
}

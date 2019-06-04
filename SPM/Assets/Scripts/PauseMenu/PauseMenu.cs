using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   
    public static bool IsPaused;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject uIHud;
    [SerializeField]
    private GameObject helpUIHud;


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
        EventHandler.Instance.Register(EventHandler.EventType.UIButtonEvent, CheckButtonClick);
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        uIHud.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
        Manager.Instance.IsPaused = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        uIHud.SetActive(false);
        helpUIHud.SetActive(false);
        Time.timeScale = 0f;
        IsPaused = true;
        Manager.Instance.IsPaused = true;
    }

    private void CheckButtonClick(BaseEventInfo e)
    {
        var buttonEventInfo = e as UIButtonEventInfo;
        if (buttonEventInfo != null)
        {
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

        }
    }


    private void ResumeGameButton()
    {

        Resume();
    }

    private void OptionsButton()
    {
        Debug.Log("Options");
    }

    private void SaveAndQuitButton()
    {
        Debug.Log("Save and quit");
        SaveEventInfo saveEventInfo = new SaveEventInfo("Saving...");
        EventHandler.Instance.FireEvent(EventHandler.EventType.SaveEvent, saveEventInfo);
        SaveSystem.SaveGame(new GameData());

        SceneManager.LoadScene(0);

    }
}

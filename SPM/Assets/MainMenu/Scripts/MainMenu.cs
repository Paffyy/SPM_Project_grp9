using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen;

    void Start()
    {
        Register();
    }

    void Register()
    {
        Debug.Log(EventHandler.Instance);
        EventHandler.Instance.Register(EventHandler.EventType.UIButtonEvent, CheckButtonClick);
    }

    private void CheckButtonClick(BaseEventInfo e)
    {
        var buttonEventInfo = e as UIButtonEventInfo;

        if (buttonEventInfo != null)
        {
            switch ((int)buttonEventInfo.ButtonEnum)
            {
                case 0:
                    NewGame();
                    break;
                case 1:
                    LoadGame();
                    break;
                case 2:
                    OptionsButton();
                    break;
                case 3:
                    Quit();
                    break;
            }

        }
    }

    private void LoadGame()
    {
        loadingScreen.SetActive(true);
        Manager.Instance.HasLoadedFromSave = true;
        SceneManager.LoadSceneAsync(SaveSystem.LoadGame().CurrentSceneIndex);
    }

    private void NewGame()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }

    private void Resume() { 

    }

    private void OptionsButton()
    {

    }

    private void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

}

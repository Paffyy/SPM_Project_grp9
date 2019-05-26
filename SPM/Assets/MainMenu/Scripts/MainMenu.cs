using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
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
                    //Resume borde vara här sen när den implementeras
                    //Resume();
                    NewGame();
                    break;
                case 1:
                    OptionsButton();
                    break;
                case 2:
                    Quit();
                    break;
                case 3:
                    NewGame();
                    break;
            }

        }
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

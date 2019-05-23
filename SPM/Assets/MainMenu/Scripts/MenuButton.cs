using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public MenuButtonController MenuButtonController;
    public Animator Animator;
    public AnimatorFunctions AnimatorFunctions;
    public int ThisIndex;

    // Update is called once per frame
    void Update()
    {
        if(MenuButtonController.Index == ThisIndex)
        {
            Animator.SetBool("selected", true);
            if(InputManager.Instance.GetkeyDown(KeybindManager.Instance.MenuSelect, InputManager.ControllMode.Menu))
            {
                Animator.SetTrigger("pressed");

            } else if (Animator.GetBool("pressed"))
            {
                Animator.SetTrigger("pressed");
                AnimatorFunctions.DisableOnce = true;
            }
        } else
        {
            Animator.SetBool("selected", false);
        }
    }

    void fireEvent()
    {


        UIButtonEventInfo.Menu menu;
        UIButtonEventInfo.Buttons button;
        //main menu
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            menu = UIButtonEventInfo.Menu.MainMenu;
        }
        //alla andra scener
        else
        {
            menu = UIButtonEventInfo.Menu.PauseMenu;
        }

        button = (UIButtonEventInfo.Buttons)MenuButtonController.Index;

        UIButtonEventInfo info = new UIButtonEventInfo(menu, button);

        Debug.Log("fireEvent button " + menu.ToString() + " " + button.ToString() + " build index " + SceneManager.GetActiveScene().buildIndex);

        EventHandler.Instance.FireEvent(EventHandler.EventType.UIButtonEvent, info);
    }
}

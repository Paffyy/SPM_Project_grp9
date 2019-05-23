using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEventInfo : BaseEventInfo
{
    public Buttons ButtonEnum;
    public Menu MenuEnum;
    public enum Buttons
    {
        Resume = 0,
        Options = 1,
        SaveAndQuit = 2,
        NewGame = 3,
    }

    public enum Menu
    {
        MainMenu = 0,
        PauseMenu = 1,
    }

    public UIButtonEventInfo(Menu menu, Buttons buttonEnum)
    {
        this.ButtonEnum = buttonEnum;
    }
}

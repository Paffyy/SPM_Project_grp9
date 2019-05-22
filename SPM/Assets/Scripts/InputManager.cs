using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{

    public enum ControllMode
    {
        Play = 0,
        Menu = 1,
        AllStates = 2,
    }

    public enum GetAxis
    {
        Horizontal = 0,
        Vertical = 1,
        MouseY = 2,
        MouseX = 3,
    }
    private string[] axisStrings = new string[] { "Horizontal", "Vertical", "Mouse Y", "Mouse X" };


    string TranslateEnum(GetAxis axis)
    {
        return axisStrings[(int)axis];
         
    }

    public bool GetkeyDown(Keybind key, ControllMode mode)
    {
        if (UseMenuControlls(mode) == true)
            return false;
        else
            return GetKeyDownFromKeybind(key);
    }

    public bool GetkeyUp(Keybind key, ControllMode mode)
    {
        if (UseMenuControlls(mode) == true)
            return false;
        else
            return GetKeyUpFromKeyBind(key);
    }

    public bool Getkey(Keybind key, ControllMode mode)
    {
        if (UseMenuControlls(mode) == true)
            return false;
        else
            return GetKeyDownFromKeybind(key);
    }

    private bool GetKeyDownFromKeybind(Keybind key)
    {
        foreach (KeyCode code in key.KeybindCodes)
        {
            if (Input.GetKeyDown(code) == true)
                return true;
        }
        return false;
    }

    //Denna metod verkar inte fungera, uniy verkar göra något speciellt med deras GetAxis så den beter sig lite kostigt
    public float GetAxisRaw(GetAxis axis, ControllMode mode)
    {
        if (UseMenuControlls(mode) == true)
            return 0;
        else
            return Input.GetAxisRaw(TranslateEnum(axis));
    }

    private bool GetKeyUpFromKeyBind(Keybind key)
    {
        foreach (KeyCode code in key.KeybindCodes)
        {
            if (Input.GetKeyUp(code) == true)
                return true;
        }
        return false;
    }

    private bool GetKeyFromKeyBind(Keybind key)
    {
        foreach (KeyCode code in key.KeybindCodes)
        {
            if (Input.GetKey(code) == true)
                return true;
        }
        return false;
    }

    private bool UseMenuControlls(ControllMode mode)
    {
        if (mode == ControllMode.AllStates && mode == ControllMode.Menu && Manager.Instance.IsPaused == true)
            return true;
        return false;
    }

    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InputManager();
            return instance;
        }
    }

}

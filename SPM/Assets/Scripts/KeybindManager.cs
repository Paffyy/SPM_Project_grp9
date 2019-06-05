//Author: Niclas Älmeby och Patrik Wåhlin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindManager
{
    private KeybindManager()
    {
        // Declare here (probably move to keybinds menu l8er)
        SpecialAttack = new Keybind(new KeyCode[] { KeyCode.E, KeyCode.JoystickButton2 }, "Perform a special attack with equiped weapon");
        ShootAndAttack = new Keybind(new KeyCode[] { KeyCode.Mouse0, KeyCode.JoystickButton10 }, "Perform a regular attack with equiped weapon");
        BlockAndAim = new Keybind(new KeyCode[] { KeyCode.Mouse1, KeyCode.JoystickButton9 }, "Block with shield or aim bow");
        EquipmentSlot1 = new Keybind(new KeyCode[] { KeyCode.Alpha1, KeyCode.JoystickButton1 });
        EquipmentSlot2 = new Keybind(new KeyCode[] { KeyCode.Alpha2, KeyCode.JoystickButton3 });
        EquipmentSlot3 = new Keybind(new KeyCode[] { KeyCode.Alpha3 });
        PauseMenuButton = new Keybind(new KeyCode[] { KeyCode.Escape, KeyCode.JoystickButton7 });
        Jump = new Keybind(new KeyCode[] { KeyCode.Space, KeyCode.JoystickButton0});
        MenuUp = new Keybind(new KeyCode[] { KeyCode.W, KeyCode.UpArrow });
        MenuDown = new Keybind(new KeyCode[] { KeyCode.S, KeyCode.DownArrow });
        MenuSelect = new Keybind(new KeyCode[] { KeyCode.Space, KeyCode.Return});
    }

    private static KeybindManager instance;
    public static KeybindManager Instance
    {
        get
        {
            if (instance == null)
                instance = new KeybindManager();
            return instance;
        }
    }
    // Keybinds
    public Keybind SpecialAttack { get; set; }
    public Keybind ShootAndAttack { get; set; }
    public Keybind EquipmentSlot1 { get; set; }
    public Keybind EquipmentSlot2 { get; set; }
    public Keybind EquipmentSlot3 { get; set; }
    public Keybind BlockAndAim { get; set; }
    public Keybind PauseMenuButton { get; set; }
    public Keybind Jump { get; set; }
    public Keybind MenuUp { get; set; }
    public Keybind MenuDown { get; set; }
    public Keybind MenuSelect { get; set; }
}

public class Keybind
{
    string Description;
    public KeyCode[] KeybindCodes { get; }
    public Keybind(KeyCode[] code)
    {
        KeybindCodes = code;
    }
    public Keybind(KeyCode[] code, string desc)
    {
        KeybindCodes = code;
        Description = desc;
    }

}

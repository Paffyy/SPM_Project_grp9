using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindManager
{
    private KeybindManager()
    {
        // Declare here (probably move to keybinds menu l8er)
        SpecialAttack = new Keybind(KeyCode.E, "Perform a special attack with equiped weapon");
        ShootAndAttack = new Keybind(KeyCode.Mouse0, "Perform a regular attack with equiped weapon");
        EquipmentSlot1 = new Keybind(KeyCode.Alpha1);
        EquipmentSlot2 = new Keybind(KeyCode.Alpha2);
        EquipmentSlot3 = new Keybind(KeyCode.Alpha3);
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
}

public class Keybind
{
    string Description;
    KeyCode KeybindCode;
    public Keybind(KeyCode code)
    {
        KeybindCode = code;
    }
    public Keybind(KeyCode code, string desc)
    {
        KeybindCode = code;
        Description = desc;
    }
}

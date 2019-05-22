using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindManager
{
    private KeybindManager()
    {
        // Declare here (probably move to keybinds menu l8er)
        //SpecialAttack = new Keybind(KeyCode.E, "Perform a special attack with equiped weapon");
        //ShootAndAttack = new Keybind(KeyCode.Mouse0, "Perform a regular attack with equiped weapon");
        //BlockAndAim = new Keybind(KeyCode.Mouse1, "Block with shield or aim bow");
        //EquipmentSlot1 = new Keybind(KeyCode.Alpha1);
        //EquipmentSlot2 = new Keybind(KeyCode.Alpha2);
        //EquipmentSlot3 = new Keybind(KeyCode.Alpha3);
        //PauseMenuButton = new Keybind(KeyCode.Escape);
        //Jump = new Keybind(KeyCode.Space);
        //MenuDown = new Keybind(KeyCode.W);
        //MenuDown = new Keybind(KeyCode.S);

        SpecialAttack = new Keybind(new KeyCode[] { KeyCode.E }, "Perform a special attack with equiped weapon");
        ShootAndAttack = new Keybind(new KeyCode[] { KeyCode.Mouse1 }, "Perform a regular attack with equiped weapon");
        BlockAndAim = new Keybind(new KeyCode[] { KeyCode.Mouse1 }, "Block with shield or aim bow");
        EquipmentSlot1 = new Keybind(new KeyCode[] { KeyCode.Alpha1 });
        EquipmentSlot2 = new Keybind(new KeyCode[] { KeyCode.Alpha2 });
        EquipmentSlot3 = new Keybind(new KeyCode[] { KeyCode.Alpha3 });
        PauseMenuButton = new Keybind(new KeyCode[] { KeyCode.Escape });
        Jump = new Keybind(new KeyCode[] { KeyCode.Space });
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

    //public bool GetKeyDown()
    //{
    //    foreach(KeyCode code in KeybindCodes)
    //    {
    //        if (Input.GetKeyDown(code) == true)
    //            return true;
    //    }
    //    return false;
    //}

    //public bool GetKeyUp()
    //{
    //    foreach (KeyCode code in KeybindCodes)
    //    {
    //        if (Input.GetKeyUp(code) == true)
    //            return true;
    //    }
    //    return false;
    //}

    //public bool GetKey()
    //{
    //    foreach (KeyCode code in KeybindCodes)
    //    {
    //        if (Input.GetKey(code) == true)
    //            return true;
    //    }
    //    return false;
    //}

}

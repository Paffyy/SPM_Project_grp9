using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager
{
    private Manager()
    {
        // Prevent outside instantiation
    }
    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            if (instance == null)
                instance = new Manager();
            return instance;
        }
    }

}

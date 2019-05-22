using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEventInfo : BaseEventInfo
{
    string buttonName;
    public UIButtonEventInfo(string buttonName)
    {
        this.buttonName = buttonName;
    }
}

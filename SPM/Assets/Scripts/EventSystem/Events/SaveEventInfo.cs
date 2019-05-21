using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEventInfo : BaseEventInfo
{
    public string SaveGameText;
    public SaveEventInfo(string saveGameText)
    {
       SaveGameText = saveGameText;
    }
}

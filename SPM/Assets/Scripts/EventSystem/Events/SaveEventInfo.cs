using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEventInfo : BaseEventInfo
{
    public string SaveGameText { get; set; }
    public SaveEventInfo(string saveGameText)
    {
       SaveGameText = saveGameText;
    }
}

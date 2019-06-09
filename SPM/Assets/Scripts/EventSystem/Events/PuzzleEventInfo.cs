using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEventInfo : BaseEventInfo
{
    public GameObject PuzzleObject { get; set; }

    public PuzzleEventInfo(GameObject puzzleObject)
    {
        PuzzleObject = puzzleObject;
    }
}

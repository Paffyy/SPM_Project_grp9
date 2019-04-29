using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEventInfo : BaseEventInfo
{
    public List<GameObject> PuzzleObjects;
    public List<GameObject> RevitalizedObjects;
    public PuzzleEventInfo(List<GameObject> puzzleObjects, List<GameObject> revitalizeObjects)
    {
        PuzzleObjects = puzzleObjects;
        RevitalizedObjects = revitalizeObjects;
    }
}

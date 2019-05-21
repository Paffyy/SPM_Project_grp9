using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int CurrentSceneIndex;
    public float[] PlayerPosition;
    public List<float> DeadEnemiesID;
    public List<float> PickedUpObjectsID;
    public List<float> RevitalizedZonesID;

    public GameData()
    {
        CurrentSceneIndex = GameControl.GameController.CurrentSceneIndex;
        PlayerPosition = new float[3];
        PlayerPosition[0] = GameControl.GameController.Player.transform.position.x;
        PlayerPosition[1] = GameControl.GameController.Player.transform.position.y;
        PlayerPosition[2] = GameControl.GameController.Player.transform.position.z;
        DeadEnemiesID = GameControl.GameController.DeadEnemies;
        PickedUpObjectsID = GameControl.GameController.PickedUpObjects;
        RevitalizedZonesID = GameControl.GameController.RevitalizedZones;

    }
}

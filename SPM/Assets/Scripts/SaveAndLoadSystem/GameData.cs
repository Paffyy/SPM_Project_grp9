using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int CurrentSceneIndex;
    public int PlayerHealth;
    public int ArrowCount;
    public float[] PlayerPosition;
    public List<float> DeadEnemiesID;
    public List<float> PickedUpObjectsID;
    public List<float> RevitalizedZonesID;

    public GameData()
    {
        CurrentSceneIndex = GameController.GameControllerInstance.CurrentSceneIndex;
        PlayerHealth = GameController.GameControllerInstance.PlayerHealth;
        ArrowCount = GameController.GameControllerInstance.ArrowCount;
        PlayerPosition = new float[3];
        PlayerPosition[0] = GameController.GameControllerInstance.Player.position.x;
        PlayerPosition[1] = GameController.GameControllerInstance.Player.position.y;
        PlayerPosition[2] = GameController.GameControllerInstance.Player.position.z;
        DeadEnemiesID = GameController.GameControllerInstance.DeadEnemies;
        PickedUpObjectsID = GameController.GameControllerInstance.PickedUpObjects;
        RevitalizedZonesID = GameController.GameControllerInstance.RevitalizedZones;

    }
}

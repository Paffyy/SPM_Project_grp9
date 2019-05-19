using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float[] PlayerPosition;
    public List<float> DeadEnemiesID;
    public List<float> PickedUpObjectsID;
    public List<float> RevitalizedZonesID;

    public GameData(GameObject player)
    {
        PlayerPosition = new float[3];
        PlayerPosition[0] = player.transform.position.x;
        PlayerPosition[1] = player.transform.position.y;
        PlayerPosition[2] = player.transform.position.z;
        DeadEnemiesID = GameControl.GameController.DeadEnemies;
        PickedUpObjectsID = GameControl.GameController.PickedUpObjects;
        RevitalizedZonesID = GameControl.GameController.RevitalizedZones;

    }
}

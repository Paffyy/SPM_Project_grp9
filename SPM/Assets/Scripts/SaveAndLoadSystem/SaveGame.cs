using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ska förmodligen göras om till lyssnare
public class SaveGame : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        if(GameControl.GameController.hasLoadedFromSaveFile == true)
        {
            Load();
        }
    }

    void Update()
    {

    }

    private void Save()
    {
        SaveSystem.SaveGame();
    }

    private void Load()
    {
        GameData data = SaveSystem.LoadGame();

        Vector3 position;
        position.x = data.PlayerPosition[0];
        position.y = data.PlayerPosition[1];
        position.z = data.PlayerPosition[2];
        Player.transform.position = position;
        List<float> EnemiesID = data.DeadEnemiesID;
        foreach(float ID in EnemiesID)
        {
            GameControl.GameController.DeadEnemies.Add(ID);
            GameObject enemy = GameControl.GameController.Enemies[ID];
            if(enemy != null)
            {
                EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, new DeathEventInfo(enemy));
                enemy.SetActive(false);
            }
        }
        List<float> PickUpsID = data.PickedUpObjectsID;
        foreach (float ID in PickUpsID)
        {
            GameControl.GameController.PickedUpObjects.Add(ID);
            GameObject pickUp = GameControl.GameController.PickUps[ID];
            pickUp.SetActive(false);
        }
        List<float> ZonesID = data.RevitalizedZonesID;
        foreach (float ID in ZonesID)
        {
            GameControl.GameController.RevitalizedZones.Add(ID);
            GameObject zone = GameControl.GameController.Zones[ID];
            zone.GetComponent<RevitalizeZone>().RevitalizeTheZoneInstant();
        }
    }
}

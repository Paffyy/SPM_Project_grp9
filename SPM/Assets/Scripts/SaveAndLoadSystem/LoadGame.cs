using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        if (Manager.Instance.HasLoadedFromSave)
        {
            LoadFromSave();
            Manager.Instance.HasLoadedFromSave = false;
        } else if (Manager.Instance.HasLoadedFromPreviousLevel)
        {
            LoadPlayerDataOnly();
            Manager.Instance.HasLoadedFromPreviousLevel = false;
        }
    }

    private void LoadPlayerDataOnly()
    {
        GameData data = SaveSystem.LoadGame();
        Player.GetComponent<PlayerHealth>().CurrentHealth = data.PlayerHealth;
        Player.GetComponent<Weapon>().ArrowCount = data.ArrowCount;
        Player.GetComponent<Weapon>().ChangeState(data.CurrentWeaponState);
    }

    private void LoadFromSave()
    {
        GameData data = SaveSystem.LoadGame();
        Vector3 position;
        position.x = data.PlayerPosition[0];
        position.y = data.PlayerPosition[1];
        position.z = data.PlayerPosition[2];
        Player.transform.position = position;
        Player.GetComponent<Player>().RotationY = data.PlayerRotation;
        Player.GetComponent<PlayerHealth>().CurrentHealth = data.PlayerHealth;
        Player.GetComponent<Weapon>().ArrowCount = data.ArrowCount;
        Player.GetComponent<Weapon>().ChangeState(data.CurrentWeaponState);
        List<float> ZonesID = data.RevitalizedZonesID;
        foreach (float ID in ZonesID)
        {
            GameController.GameControllerInstance.RevitalizedZones.Add(ID);
            GameObject zone = GameController.GameControllerInstance.Zones[ID];
            zone.GetComponent<RevitalizeZone>().RevitalizeTheZoneInstant();
        }
        List<float> EnemiesID = data.DeadEnemiesID;
        foreach(float ID in EnemiesID)
        {
           GameController.GameControllerInstance.DeadEnemies.Add(ID);
            GameObject enemy = GameController.GameControllerInstance.Enemies[ID];
            if(enemy != null)
            {
                EventHandler.Instance.FireEvent(EventHandler.EventType.DeathEvent, new DeathEventInfo(enemy));
                enemy.SetActive(false);
            }
        }
        List<float> PickUpsID = data.PickedUpObjectsID;
        foreach (float ID in PickUpsID)
        {
            GameController.GameControllerInstance.PickedUpObjects.Add(ID);
            GameObject pickUp = GameController.GameControllerInstance.PickUps[ID];
            pickUp.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ska förmodligen göras om till lyssnare
public class SaveGame : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    private void Save()
    {
        SaveSystem.SaveGame(Player);
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
            GameObject enemy = GameControl.GameController.Enemies[ID];
            enemy.SetActive(false);
        }
        List<float> PickUpsID = data.PickedUpObjectsID;
        foreach (float ID in PickUpsID)
        {
            GameObject pickUp = GameControl.GameController.PickUps[ID];
            pickUp.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointListener : MonoBehaviour
{
    public GameObject FirstCheckPoint;
    public Camera PlayerCamera;
    public GameObject Player;

    private Transform CurrentRespawnPosition;

    void Start()
    {
        CurrentRespawnPosition = FirstCheckPoint.GetComponent<CheckPoint>().RespawnPosition;
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.CheckPointEvent, UpdateCheckPoint);
        EventHandler.Instance.Register(EventHandler.EventType.DeathEvent, RespawnPlayer);
    }

    private void UpdateCheckPoint(BaseEventInfo e)
    {
        var checkPointEventInfo = e as CheckPointEventInfo;
        if (checkPointEventInfo != null)
        {
            CurrentRespawnPosition = checkPointEventInfo.CheckPoint.GetComponent<CheckPoint>().RespawnPosition;
            GameData data = new GameData();
            data.PlayerPosition[0] = CurrentRespawnPosition.position.x;
            data.PlayerPosition[1] = CurrentRespawnPosition.position.y;
            data.PlayerPosition[2] = CurrentRespawnPosition.position.z;
            data.PlayerRotation = CurrentRespawnPosition.rotation.y + 90;
            data.PlayerHealth = Player.GetComponent<PlayerHealth>().StartingHealth;
            if (data.ArrowCount < 5)
                data.ArrowCount = 5;
            SaveSystem.SaveGame(data);
            SaveEventInfo saveEventInfo = new SaveEventInfo("Reached new checkpoint! auto saving...");
            EventHandler.Instance.FireEvent(EventHandler.EventType.SaveEvent, saveEventInfo);
        }
    }

    private void RespawnPlayer(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if (deathEventInfo != null)
        {
            if (deathEventInfo.GameObject.CompareTag("Player"))
            {
                Manager.Instance.HasLoadedFromSave = true;
                SceneManager.LoadScene(GameController.GameControllerInstance.CurrentSceneIndex);

                //deathEventInfo.GameObject.transform.position = CurrentRespawnPosition.transform.position;
                //deathEventInfo.GameObject.GetComponent<Player>().RotationY = CurrentRespawnPosition.rotation.y + 90;
                //PlayerCamera.transform.rotation = Quaternion.Euler(PlayerCamera.transform.rotation.x, CurrentRespawnPosition.rotation.y, PlayerCamera.transform.rotation.z);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointListener : MonoBehaviour
{
    [SerializeField]
    private GameObject firstCheckPoint;
    [SerializeField]
    private GameObject player;
    private Transform currentRespawnPosition;

    void Start()
    {
        currentRespawnPosition = firstCheckPoint.GetComponent<CheckPoint>().RespawnPosition;
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
            currentRespawnPosition = checkPointEventInfo.CheckPoint.GetComponent<CheckPoint>().RespawnPosition;
            GameData data = new GameData();
            data.PlayerPosition[0] = currentRespawnPosition.position.x;
            data.PlayerPosition[1] = currentRespawnPosition.position.y;
            data.PlayerPosition[2] = currentRespawnPosition.position.z;
            data.PlayerHealth = player.GetComponent<PlayerHealth>().StartingHealth;
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
            }
        }
    }
}

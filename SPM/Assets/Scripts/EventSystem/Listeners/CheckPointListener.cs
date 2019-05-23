using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointListener : MonoBehaviour
{
    public GameObject FirstCheckPoint;
    public Camera PlayerCamera;
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
        }
    }

    private void RespawnPlayer(BaseEventInfo e)
    {
        var deathEventInfo = e as DeathEventInfo;
        if (deathEventInfo != null)
        {
            if (deathEventInfo.GameObject.CompareTag("Player"))
            {
                Manager.Instance.HasLoadedFromCheckPoint = true;
                SceneManager.LoadScene(GameController.GameControllerInstance.CurrentSceneIndex);

                //deathEventInfo.GameObject.transform.position = CurrentRespawnPosition.transform.position;
                //deathEventInfo.GameObject.GetComponent<Player>().RotationY = CurrentRespawnPosition.rotation.y + 90;
                //PlayerCamera.transform.rotation = Quaternion.Euler(PlayerCamera.transform.rotation.x, CurrentRespawnPosition.rotation.y, PlayerCamera.transform.rotation.z);
            }
        }
    }
}

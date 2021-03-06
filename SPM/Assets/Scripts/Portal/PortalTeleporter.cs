﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporter : MonoBehaviour
{
    public Transform Player;
    public Transform Receiver;
    [SerializeField]
    private GameObject loadingScreen;
    private bool playerIsOverlapping = false;
    private bool hasLoadedScene = false;

    void Update()
    {
        if (playerIsOverlapping)
        {
            if (!hasLoadedScene)
            {
                hasLoadedScene = true;
                Manager.Instance.HasLoadedFromPreviousLevel = true;
                SaveSystem.SaveGame(new GameData());
                loadingScreen.SetActive(true);
                SceneManager.LoadSceneAsync(3);
            }
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsOverlapping = false;
        }
    }
}

#region old update
//Vector3 portalToPlayer = Player.position - transform.position;
//float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
//if(dotProduct < 0f)
//{
//    float rotationDiff = -Quaternion.Angle(transform.rotation, Receiver.rotation);
//    rotationDiff += 180;
//    Player.Rotate(Vector3.up, rotationDiff);
//    Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
//    Player.position = new Vector3(Receiver.position.x, Receiver.position.y + 0.1f, Receiver.position.z) + positionOffset;
//    playerIsOverlapping = false;
//}
#endregion

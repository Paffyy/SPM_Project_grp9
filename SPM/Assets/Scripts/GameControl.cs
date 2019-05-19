using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl GameController;
    public int PlayerHealth;
    public float[] PlayerPosition;
    public List<float> DeadEnemies;
    public List<float> PickedUpObjects;
    public List<float> RevitalizedZones;
    public Dictionary<float, GameObject> Enemies;
    public Dictionary<float, GameObject> PickUps;
    public Dictionary<float, GameObject> Zones;

    void Awake()
    {
        if (GameController == null)
        {
            DontDestroyOnLoad(gameObject);
            GameController = this;
        }
        else if (GameController != this)
        {
            Destroy(gameObject);
        }
        DeadEnemies = new List<float>();
        Enemies = new Dictionary<float, GameObject>();
        PickedUpObjects = new List<float>();
        PickUps = new Dictionary<float, GameObject>();
        RevitalizedZones = new List<float>();
        Zones = new Dictionary<float, GameObject>();
    }

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl GameController;
    public int CurrentSceneIndex { get { return SceneManager.GetActiveScene().buildIndex; } }
    public Player Player { get { return FindObjectOfType<Player>(); } }
    public int PlayerHealth { get { return FindObjectOfType<PlayerHealth>().CurrentHealth; } }
    public float[] PlayerPosition;
    public List<float> DeadEnemies;
    public List<float> PickedUpObjects;
    public List<float> RevitalizedZones;
    public Dictionary<float, GameObject> Enemies;
    public Dictionary<float, GameObject> PickUps;
    public Dictionary<float, GameObject> Zones;
    public bool hasLoadedFromSaveFile;

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
        //Player = FindObjectOfType<Player>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveEventInfo saveEventInfo = new SaveEventInfo("Saving...");
            EventHandler.Instance.FireEvent(EventHandler.EventType.SaveEvent, saveEventInfo);
            SaveSystem.SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        GameData data = SaveSystem.LoadGame();
        if(data != null)
        {
            hasLoadedFromSaveFile = true;
            ClearController();
            SceneManager.LoadScene(data.CurrentSceneIndex);
        } else
        {
            Debug.Log("Error, no data file to load from");
        }
    }

    private void ClearController()
    {
        DeadEnemies.Clear();
        PickedUpObjects.Clear();
        RevitalizedZones.Clear();
        Enemies.Clear();
        PickUps.Clear();
        Zones.Clear();
    }
}

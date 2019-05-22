using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController GameControllerInstance;
    public int CurrentSceneIndex { get { return SceneManager.GetActiveScene().buildIndex; } }
    public Transform Player { get { return player.transform; } }
    public int PlayerHealth { get { return playerHealthScript.CurrentHealth; } }
    public int ArrowCount { get { return weaponScript.ArrowCount; } }
    public List<float> DeadEnemies = new List<float>();
    public List<float> PickedUpObjects = new List<float>();
    public List<float> RevitalizedZones = new List<float>();
    public Dictionary<float, GameObject> Enemies = new Dictionary<float, GameObject>();
    public Dictionary<float, GameObject> PickUps = new Dictionary<float, GameObject>();
    public Dictionary<float, GameObject> Zones = new Dictionary<float, GameObject>();

    private PlayerHealth playerHealthScript;
    private Weapon weaponScript;
    [SerializeField]
    private GameObject player;

    void Awake()
    {
        GameControllerInstance = this;
        weaponScript = player.GetComponent<Weapon>();
        playerHealthScript = player.GetComponent<PlayerHealth>();
        DeadEnemies = new List<float>();
        Enemies = new Dictionary<float, GameObject>();
        PickedUpObjects = new List<float>();
        PickUps = new Dictionary<float, GameObject>();
        RevitalizedZones = new List<float>();
        Zones = new Dictionary<float, GameObject>();
    }

    void Start()
    {
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    SaveEventInfo saveEventInfo = new SaveEventInfo("Saving...");
        //    EventHandler.Instance.FireEvent(EventHandler.EventType.SaveEvent, saveEventInfo);
        //    SaveSystem.SaveGame();
        //}
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(Manager.Instance.HasLoadedFromCheckPoint);
            Manager.Instance.HasLoadedFromCheckPoint = true;
            SceneManager.LoadScene(2);
        }
    }
}

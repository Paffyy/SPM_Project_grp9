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
    public int CurrentWeaponState { get { return weaponScript.CurrentStateID; } }
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
    }
}

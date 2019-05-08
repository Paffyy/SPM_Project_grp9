using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorFunctions : MonoBehaviour
{
    public MenuButtonController MenuButtonController;
    public bool DisableOnce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlaySound(AudioClip soundClip)
    {
        if (!DisableOnce)
        {
            MenuButtonController.AudioSource.PlayOneShot(soundClip);
        } else
        {
            DisableOnce = false;
        }
    }

    void LoadNewScene()
    {
        if(MenuButtonController.Index == 0)
        {
            SceneManager.LoadScene(1);
        } else if(MenuButtonController.Index == MenuButtonController.MaxIndex)
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}

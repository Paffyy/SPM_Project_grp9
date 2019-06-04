using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFirstLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject FadeIn;
    [SerializeField]
    private GameObject Tutorial;

    private void Awake()
    {
        if (Manager.Instance.HasLoadedFromSave)
        {
            FadeIn.SetActive(false);
            Tutorial.SetActive(false);
        }
    }


}

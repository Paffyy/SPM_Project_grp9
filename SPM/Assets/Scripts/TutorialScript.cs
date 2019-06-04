using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private float terminationDelay;
    [SerializeField]
    private GameObject TutorialText;
    [SerializeField]
    private bool isShownOnStart;
    [SerializeField]
    private float shownOnStartTimeOffset;

    private IEnumerator previousCoroutine;
    void Awake()
    {
        if (isShownOnStart)
        {
            StartCoroutine(ShowOnStart(shownOnStartTimeOffset));
            
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && Manager.Instance.IsPaused == false)
        {
            ShowAndRemove();
        }
    }


    private void ShowAndRemove()
    {
        var coroutine = Display(terminationDelay);
        TutorialText.SetActive(!TutorialText.activeSelf);
        if (TutorialText.activeSelf)
        {
            StartCoroutine(coroutine);
        }
        else
        {
            StopCoroutine(previousCoroutine);
        }
        previousCoroutine = coroutine;
    }

    IEnumerator Display(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TutorialText.SetActive(false);
    }

    IEnumerator ShowOnStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ShowAndRemove();
    }
}

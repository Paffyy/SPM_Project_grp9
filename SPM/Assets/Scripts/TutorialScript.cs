using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private float terminationDelay;
    [SerializeField]
    private GameObject TutorialText;

    private IEnumerator previousCoroutine;
    void Awake()
    {
        ShowAndRemove();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
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
            if (previousCoroutine != null)
            {
                StopCoroutine(previousCoroutine);
            }
        }
        previousCoroutine = coroutine;
    }

    IEnumerator Display(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TutorialText.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public float terminationDelay;
    public GameObject TutorialText;

    private IEnumerator previousCoroutine;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
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
    }
    IEnumerator Display(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TutorialText.SetActive(false);
    }
}

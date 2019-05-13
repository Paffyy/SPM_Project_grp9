using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public float terminationDelay;
    public GameObject TutorialText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TutorialText.SetActive(!TutorialText.activeSelf);
            if (TutorialText.activeSelf)
            {
                StartCoroutine(Display(terminationDelay));
            }
        }
    }
    IEnumerator Display(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TutorialText.SetActive(false);
    }
}

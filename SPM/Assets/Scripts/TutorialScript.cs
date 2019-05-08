using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public float terminationDelay;
    void Awake()
    {
        StartCoroutine(Terminate(terminationDelay));
    }
    IEnumerator Terminate(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

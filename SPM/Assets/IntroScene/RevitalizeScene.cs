using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RevitalizeScene : MonoBehaviour
{
    [SerializeField]
    private float revitalizeDelay;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private List<RevitalizeZone> revitalizeZones;
    void Start()
    {
        StartCoroutine(StartRevitalizeDelayed());
    }
    private IEnumerator StartRevitalizeDelayed()
    {
        yield return new WaitForSeconds(startDelay);
        foreach (var item in revitalizeZones)
        {
            item.DevitalizeTheZone(revitalizeDelay);
        }
    }

}

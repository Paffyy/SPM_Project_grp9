using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public GameObject WorldB;
    public GameObject PortalSystem;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WorldB.SetActive(true);
            PortalSystem.SetActive(true);
        }
    }
}

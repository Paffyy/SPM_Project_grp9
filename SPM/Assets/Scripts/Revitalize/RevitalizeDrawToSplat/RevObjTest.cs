using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevObjTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
        }
    }
}

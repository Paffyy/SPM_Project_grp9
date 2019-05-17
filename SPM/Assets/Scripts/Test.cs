using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject otherObject;
    private float radius;
    void Start()
    {
        radius = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var flankList = Manager.Instance.GetFlankingPoints(transform, otherObject.transform, radius , 15, true);
            foreach (var item in flankList)
            {
                Instantiate<GameObject>(otherObject, item + transform.forward * radius , Quaternion.identity);
            }
        }
    }

}

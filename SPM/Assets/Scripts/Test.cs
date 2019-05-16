using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var flankList = Manager.Instance.GetFlankingPoints(transform, 5, true);
            foreach (var item in flankList)
            {
                Instantiate<GameObject>(gameObject, item, Quaternion.identity);
            }
        }
    }

}

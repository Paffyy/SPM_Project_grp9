using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public int Index;
    public int MaxIndex;
    public bool KeyDown;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            if (!KeyDown)
            {
                if(Input.GetAxis("Vertical") < 0)
                {
                    if(Index < MaxIndex)
                    {
                        Index++;
                    } else
                    {
                        Index = 0;
                    }
                } else if (Input.GetAxis("Vertical") > 0)
                {
                    if(Index > 0)
                    {
                        Index--;
                    } else
                    {
                        Index = MaxIndex;
                    }
                }
                KeyDown = true;
            }
        } else
        {
            KeyDown = false;
        }
    }
}

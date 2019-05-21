using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public int Index;
    public int MaxIndex;
    public bool KeyDown;
    public AudioSource AudioSource;
    private int input;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.W))
            input = 1;
        else if (Input.GetKey(KeyCode.S))
            input = -1;
        else
            input =  0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Debug.Log(input);
        //if(Input.GetAxis("Vertical") != 0)
        if(input != 0)
        {
            if (!KeyDown)
            {
                //if(Input.GetAxis("Vertical") < 0 || Input.GetKeyDown(KeybindManager.Instance.MenuUp.GetKeyCode()))
                if(input == 1)
                {
                    if(Index < MaxIndex)
                    {
                        Index++;
                    } else
                    {
                        Index = 0;
                    }
                }
                //else if (Input.GetAxis("Vertical") > 0 || Input.GetKeyDown(KeybindManager.Instance.MenuDown.GetKeyCode()))
                else if (input == -1)
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

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

        if (InputManager.Instance.Getkey(KeybindManager.Instance.MenuDown, InputManager.ControllMode.Menu))
            input = 1;
        else if(InputManager.Instance.Getkey(KeybindManager.Instance.MenuUp, InputManager.ControllMode.Menu))
            input = -1;
        else
            input =  0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        //if(Input.GetAxis("Vertical") != 0)
        if(input != 0)
        {
            if (!KeyDown)
            {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }
    public int Index { get; set; }
    [SerializeField]
    private int maxIndex;
    private bool keyDown;
    private int input;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void CheckInput()
    {

        if (InputManager.Instance.Getkey(KeybindManager.Instance.MenuDown, InputManager.ControllMode.Menu) || Input.GetAxisRaw("VerticalDpad") == 1)
            input = 1;
        else if(InputManager.Instance.Getkey(KeybindManager.Instance.MenuUp, InputManager.ControllMode.Menu) || Input.GetAxisRaw("VerticalDpad") == -1)
            input = -1;
        else
            input =  0;
    }


    void Update()
    {
        CheckInput();
        //if(Input.GetAxis("Vertical") != 0)
        if(input != 0)
        {
            if (!keyDown)
            {
                if(input == 1)
                {
                    if(Index < maxIndex)
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
                        Index = maxIndex;
                    }
                }
                keyDown = true;
            }
        } else
        {
            keyDown = false;
        }
    }
}

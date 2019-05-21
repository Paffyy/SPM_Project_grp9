using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public MenuButtonController MenuButtonController;
    public Animator Animator;
    public AnimatorFunctions AnimatorFunctions;
    public int ThisIndex;

    // Update is called once per frame
    void Update()
    {
        if(MenuButtonController.Index == ThisIndex)
        {
            Animator.SetBool("selected", true);
            if(Input.GetKey(KeyCode.Space))
            {
                Animator.SetBool("pressed", true);
            } else if (Animator.GetBool("pressed"))
            {
                Animator.SetBool("pressed", false);
                AnimatorFunctions.DisableOnce = true;
            }
        } else
        {
            Animator.SetBool("selected", false);
        }
    }
}

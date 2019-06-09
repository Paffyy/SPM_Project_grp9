using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlockAnim : MonoBehaviour
{
    private Animator shieldBlockAnimator;

    private void Start()
    {
        shieldBlockAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            shieldBlockAnimator.SetBool("IsBlocking", true);
        }
    }

    public void GoToIdle()
    {
        shieldBlockAnimator.SetBool("IsBlocking", false);
    }

    void Block()
    {
        shieldBlockAnimator.SetBool("IsBlocking", true);
    }
}

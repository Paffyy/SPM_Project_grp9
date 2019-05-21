using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTextAnimFunction : MonoBehaviour
{

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void DeactivateText()
    {
        anim.SetBool("IsSaving", false);
        gameObject.SetActive(false);
    }
}

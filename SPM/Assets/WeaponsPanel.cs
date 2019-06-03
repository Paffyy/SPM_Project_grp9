using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPanel : MonoBehaviour
{
    [SerializeField] private Animator bow;
    [SerializeField] private Animator sword;


    /// <summary>
    /// Select weapon of id
    /// Sword = 1, Bow = 2
    /// </summary>
    /// <param name="id">The id of the weapon to select</param>
    /// <param name="state">The state of the selected weapon</param>
    public void SelectWeapon(int id, bool state)
    {
        if (id == 1)
            sword.SetBool("Select", state);
        else
            bow.SetBool("Select", state);
    }
}

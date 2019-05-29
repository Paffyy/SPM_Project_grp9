using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitEventInfo : BaseEventInfo
{
    public enum Weapon
    {
        Sword,
        Bow,
    }

    public Transform self;
    public Collider TargetHit;
    public Weapon WeapondsUsed;

    public AttackHitEventInfo(Transform self, Collider hit, Weapon weapon)
    {
        this.self = self;
        TargetHit = hit;
        WeapondsUsed = weapon;
    }
}

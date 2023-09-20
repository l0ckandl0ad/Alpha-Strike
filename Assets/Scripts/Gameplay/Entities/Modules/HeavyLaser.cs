using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeavyLaser : DirectWeapon
{
    public HeavyLaser()
    {
        minDamage = 180f;
        maxDamage = 200f;

        accuracy = 0.1f;

        minRange = 0f;
        maxRange = 50f;

        rateOfFire = 30f;
    }
}

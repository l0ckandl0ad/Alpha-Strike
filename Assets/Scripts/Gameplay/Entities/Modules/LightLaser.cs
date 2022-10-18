using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightLaser : DirectWeapon
{
    public LightLaser()
    {
        minDamage = 4f;
        maxDamage = 8f;

        accuracy = 0.95f;

        minRange = 0f;
        maxRange = 50f;

        rateOfFire = 5f;
    }
}

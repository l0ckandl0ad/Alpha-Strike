using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Laser : DirectWeapon
{
    public Laser()
    {
        minDamage = 10f;
        maxDamage = 20f;

        accuracy = 0.95f;

        minRange = 0f;
        maxRange = 200f;

        rateOfFire = 15f;
    }
}

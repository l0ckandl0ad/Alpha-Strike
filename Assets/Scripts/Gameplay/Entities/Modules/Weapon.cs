using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon : Module, IWeapon
{
    public Weapon()
    {
        Name = "Weapon";
        Type = ModuleType.Weapon;
    }

    public virtual void Fire(TargetData target)
    {
        
    }


}

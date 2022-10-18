using System.Collections.Generic;
[System.Serializable]
public abstract class CombatVessel : Vessel, ICombatant, ISensorPlatform
{
    public List<IWeapon> Weapons { get; protected set; } = new List<IWeapon>();
    public List<IModule> Sensors { get; protected set; } = new List<IModule>();

}

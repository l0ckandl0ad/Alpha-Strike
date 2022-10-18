using System;

[Serializable]
public abstract class Vessel : SpacePlatform, IVessel
{
    public string Class { get; protected set; }
    public string HullName { get; protected set; }
    public float MaxSpeed { get; protected set; } = 30f;
}

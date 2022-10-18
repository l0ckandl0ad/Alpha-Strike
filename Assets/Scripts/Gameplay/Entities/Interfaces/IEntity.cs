using System;
/// <summary>
/// Represents a singular simulation entity in virtual space, eg ship, missile, grain of stardust etc
/// </summary>
public interface IEntity
{
    string Name { get; }
    bool IsAlive { get; }
    IFF IFF { get; }
    /// <summary>
    /// MapEntity that this entity is a part of. WARNING! Use [field:NonSerialized] for this field!
    /// </summary>
    IMapEntity MapEntity { get; }
    /// <summary>
    /// A generic event that allows outsiders to check in when somethin's changed on this entity.
    /// WARNING! Events should NOT be SERIALIZED, because register for them during runtime!
    /// Use [field:NonSerialized] for this field!
    /// </summary>
    event Action OnStatusChange;
    /// <summary>
    /// Binds this entity to MapEntity it is a part of.
    /// </summary>
    void BindToMapEntity(IMapEntity mapEntity);
}
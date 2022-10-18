using System;
/// <summary>
/// This entity can be targeted in combat.
/// </summary>
public interface ITargetable : IEntity
{
    int MinSize { get; } // minimum object site projection/diameter but in what unit of measurement? meters?
    int MaxSize { get; } // max size projection (in least favourable facing/conditions)
    int Structure { get; } // structural integrity, ie ship falls appart when it goes to 0 -- WIP not 0-100 for now!
    //int Systems { get; } // various systems that affect vessel's performance (0-100)
    //int Engineering { get; } // power and propulsion (0-100)
    void TakeDamage(int damage);
}

using System.Collections.Generic;
/// <summary>
/// This entity can target targetable entities in combat.
/// </summary>
public interface ICombatant : IEntity
{
    List<IWeapon> Weapons { get; }
}

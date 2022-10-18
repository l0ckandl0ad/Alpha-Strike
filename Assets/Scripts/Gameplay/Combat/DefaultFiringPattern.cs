using System.Collections.Generic;
using UnityEngine;

public class DefaultFiringPattern : IFiringPattern
{
    /// <summary>
    /// Current implementation: fire on first target on the list with all weapons there are.
    /// </summary>
    /// <param name="combatants"></param>
    /// <param name="targets"></param>
    public virtual void Fire(List<ICombatant> combatants, List<TargetData> targets)
    {
        if (combatants?.Count > 0 && targets?.Count > 0)
        {
            TargetData target = targets[0];

            foreach (ICombatant combatant in combatants)
            {
                FireAtTarget(combatant, target);
            }
        }
    }

    private void FireAtTarget(ICombatant combatant, TargetData target)
    {
        if (combatant != null && combatant.IsAlive && target.TargetableEntity.IsAlive && combatant.Weapons != null)
        {
            foreach (IWeapon weapon in combatant.Weapons)
            {
                if (weapon.IsOperational)
                {
                    weapon.Fire(target);
                }
            }
        }
    }
}

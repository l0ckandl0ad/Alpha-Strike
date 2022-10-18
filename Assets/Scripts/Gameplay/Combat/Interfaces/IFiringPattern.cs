using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFiringPattern
{
    void Fire(List<ICombatant> combatants, List<TargetData> targets);
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace AlphaStrike.Gameplay.Combat
{
    public static class CombatUtils
    {
        /// <summary>
        /// Get a list of alive ICombatants from a list of IEntities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ICombatant> EntityToCombatantList(List<IEntity> entities)
        {
            if (entities?.Count > 0)
            {
                List<ICombatant> combatants = new List<ICombatant>();
                foreach (IEntity entity in entities)
                {
                    if (entity.IsAlive && entity is ICombatant combatant)
                    {
                        combatants.Add(combatant);
                    }
                }
                return combatants;
            }
            else return null;
        }
        /// <summary>
        /// Get a list of ITargetables from a list of IEntities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ITargetable> GetTargetableList(List<IEntity> entities)
        {
            if (entities?.Count > 0)
            {
                List<ITargetable> targets = new List<ITargetable>();
                foreach (IEntity entity in entities)
                {
                    if (entity.IsAlive && entity is ITargetable targetable)
                    {
                        targets.Add(targetable);
                    }
                }
                return targets;
            }
            else return null;
        }
        /// <summary>
        /// Generates a list of TargetData structs for given observer from a list of detected targets
        /// </summary>
        /// <param name="observer">The IMapEntity whos point of view is used in TargetData creation.</param>
        /// <param name="targets">List of IMapEntities which MAY contain individual targetable IEntities.</param>
        /// <returns></returns>
        public static List<TargetData> GetTargetDataList(IMapEntity observer, List<IMapEntity> targets)
        {
            List<TargetData> targetDataList = new List<TargetData>();
            foreach (IMapEntity target in targets)
            {
                foreach (IEntity entity in target.EntityList)
                {
                    if (entity.IsAlive && entity is ITargetable targetableEntity)
                    {
                        targetDataList.Add(new TargetData(observer, target, targetableEntity));
                    }
                }
            }
            if (targetDataList.Count > 0)
            {
                return targetDataList;
            }
            else
            {
                return null;
            }
            
        }

    }
}



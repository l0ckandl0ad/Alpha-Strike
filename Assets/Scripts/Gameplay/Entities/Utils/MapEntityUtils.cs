using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AlphaStrike.Gameplay.Entities
{
    public static class MapEntityUtils
    {
        public static void RemoveDeadEntitiesFromMapEntity(IMapEntity mapEntity)
        {
            List<IEntity> deadEntitiesList = new List<IEntity>();
            foreach (IEntity entity in mapEntity.EntityList)
            {
                if (!entity.IsAlive)
                {
                    deadEntitiesList.Add(entity);
                }
            }
            if (deadEntitiesList.Count > 0)
            {
                mapEntity.RemoveEntities(deadEntitiesList);
            }
        }

        public static float GetMaxSpeed(IMovable mapEntity)
        {
            float speed = 0f;
            List<IVessel> vessels = EntityUtils.EntityToVesselList(mapEntity.EntityList);
            if (vessels.Count > 0)
            {
                List<IVessel> sortedVessels = vessels.OrderBy(vessel => vessel.MaxSpeed).ToList();
                speed = sortedVessels[0].MaxSpeed;
            }
            return speed;
        }
    }
}


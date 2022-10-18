using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaStrike.Gameplay.Entities
{
    public static class EntityUtils
    {
        public static List<IEntity> VesselToEntityList(List<IVessel> shipList, bool isAlive = true)
        {
            List<IEntity> output = new List<IEntity>();

            foreach (IVessel ship in shipList)
            {
                if (ship.IsAlive == isAlive)
                {
                    output.Add(ship);
                }
            }

            return output;
        }
        public static List<IVessel> EntityToVesselList(List<IEntity> entityList, bool isAlive = true)
        {
            List<IVessel> output = new List<IVessel>();

            foreach (IEntity entity in entityList)
            {
                if (entity is IVessel vessel && vessel.IsAlive == isAlive)
                {
                    output.Add(vessel);
                }
            }

            return output;
        }

        public static List<ISpacePlatform> EntityToSpacePlatformList(List<IEntity> entityList, bool isAlive = true)
        {
            List<ISpacePlatform> output = new List<ISpacePlatform>();

            foreach (IEntity entity in entityList)
            {
                if (entity is ISpacePlatform platform && platform.IsAlive == isAlive)
                {
                    output.Add(platform);
                }
            }

            return output;
        }
    }
}



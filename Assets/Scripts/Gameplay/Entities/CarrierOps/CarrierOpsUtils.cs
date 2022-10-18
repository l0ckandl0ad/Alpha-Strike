using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AlphaStrike.Gameplay.CarrierOps
{
    public static class CarrierOpsUtils
    {
        /// <summary>
        /// Max distance in map units for transform.position/Vector2.Distance check.
        /// </summary>
        private static float maxDistanceFromCarrierToLand = 10f;

        /// <summary>
        /// Shorthand for testing if craft is close enough to carrier to be able to land.
        /// </summary>
        public static bool IsWithinLandingDistance(IMapEntity first, IMapEntity second)
        {
            bool result = false;
            float distance = Vector2.Distance(first.Transform.position, second.Transform.position);
            if (distance <= maxDistanceFromCarrierToLand) result = true;
            Debug.Log("Landing Distance: " + distance);
            return result;
        }

        public static List<ICarrierCapable> ParseFacilitiesForCraft(List<ICarrierFacility> carrierFacilities,
            bool isAlive = true)
        {
            List<ICarrierCapable> craftList = new List<ICarrierCapable>();

            foreach (ICarrierFacility facility in carrierFacilities)
            {
                if (facility is ICarrierCraftHoldingFacility craftHoldingFacility
                    && craftHoldingFacility.CarrierCraftList.Count > 0)
                {
                    foreach (ICarrierCapable craft in craftHoldingFacility.CarrierCraftList)
                    {
                        if (isAlive && craft.IsAlive)
                        {
                            craftList.Add(craft);
                        }
                        if (!isAlive && !craft.IsAlive)
                        {
                            craftList.Add(craft);
                        }
                    }
                }
            }

            return craftList;
        }
        public static void RemoveDestroyedCraftFromFacilities(List<ICarrierFacility> carrierFacilities)
        {
            List<ICarrierCapable> craftToRemove = ParseFacilitiesForCraft(carrierFacilities, false);

            if (craftToRemove.Count > 0)
            {
                foreach(ICarrierCapable craft in craftToRemove)
                {
                    craft.CurrentHoldingFacility.RemoveCraftFromHere(craft);
                }
            }
        }

        public static List<ICarrierCapable> GetAllCraftEligibleForSearch(List<ICarrierFacility> carrierFacilities)
        {
            List<ICarrierCapable> craftForSearch = ParseFacilitiesForCraft(carrierFacilities, true);

            if (craftForSearch.Count > 0)
            {
                craftForSearch = craftForSearch.Where(x => x.IsAllowedForSearchOps == true
                && x.IsSortied == false).ToList();
            }

            return craftForSearch;
        }
    }
}
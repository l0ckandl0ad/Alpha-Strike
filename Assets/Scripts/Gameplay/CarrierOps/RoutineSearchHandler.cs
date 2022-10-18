using AlphaStrike.Gameplay.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AlphaStrike.Gameplay.CarrierOps
{
    public class RoutineSearchHandler
    {
        const float MinimumSearchRange = 500f;
        private List<ISortieData> sortiesPendingList;
        private IRoutineCarrierOpsSettings settings;
        private float[,] searchSectors;
        private DateTime nextSearchTime;

        public RoutineSearchHandler(IRoutineCarrierOpsSettings settings)
        {
            sortiesPendingList = new List<ISortieData>();
            this.settings = settings;
            nextSearchTime = DateTimeModel.CurrentDateTime;
        }

        private bool IsItTimeForASearchSortie()
        {
            bool result = false;
            if (DateTimeModel.CurrentDateTime >= nextSearchTime && settings.SearchFrequencyInMinutes > 0)
            {
                result = true;
            }
            return result;
        }

        private void GenerateSearchSorties()
        {
            sortiesPendingList.Clear();
            searchSectors = settings.GetSearchSectors();

            for (int i=0; i<32; i++) // generate up to 32 search sorties
            {   // if within minimum range and sector is marked with 1f (gotten from converted bool sector toggle)
                if (settings.Range >= MinimumSearchRange && searchSectors[i,0] == 1f)
                {
                    sortiesPendingList.Add(new SearchSortie(settings.Range, searchSectors[i, 1]));
                }
            }
        }

        private void AssignAndLaunchSorties(ICarrierOpsHandler carrierOpsHandler)
        {
            List<ICarrierCapable> searchCraftList = CarrierOpsUtils.GetAllCraftEligibleForSearch(carrierOpsHandler.
                AllCarrierFacilities); // get all available craft

            if (searchCraftList.Count == 0) return; // do nothing if there's no craft available

            if (searchCraftList.Count >= sortiesPendingList.Count) // if there's enough craft to assign all sorties...
            {
                for (int i = 0; i < sortiesPendingList.Count; i++)
                {
                    searchCraftList[i].AssignSortie(sortiesPendingList[i]); // assign them all
                    new LaunchCraft(carrierOpsHandler, searchCraftList[i], true); // and launch
                }
                sortiesPendingList.Clear(); // then clear the sorties list
            }
            else // otherwise...
            { 
                List<ISortieData> sortiesAssigned = new List<ISortieData>(); // remember what sorties we assign

                for (int i = 0; i < searchCraftList.Count; i++) //  iterate over available craft
                {
                    searchCraftList[i].AssignSortie(sortiesPendingList[i]); // assign sorties
                    sortiesAssigned.Add(sortiesPendingList[i]);
                    new LaunchCraft(carrierOpsHandler, searchCraftList[i], true); // and launch craft
                }
                sortiesPendingList = sortiesPendingList.Except(sortiesAssigned).ToList(); // only unassigned sorties remain
            }
        }

        private void SetNextSearchTime()
        {
            nextSearchTime = DateTimeModel.CurrentDateTime.AddMinutes(settings.SearchFrequencyInMinutes);
        }

        public void RunRoutineSearchOps(ICarrierOpsHandler carrierOpsHandler)
        {
            if (IsItTimeForASearchSortie())
            {
                GenerateSearchSorties();
                SetNextSearchTime();
            }

            if (sortiesPendingList.Count > 0)
            {
                AssignAndLaunchSorties(carrierOpsHandler);
            }
        }

        public void SearchNow(ICarrierOpsHandler carrierOpsHandler)
        {
            GenerateSearchSorties();
            SetNextSearchTime();
            if (sortiesPendingList.Count > 0)
            {
                AssignAndLaunchSorties(carrierOpsHandler);
            }
        }
    }
}
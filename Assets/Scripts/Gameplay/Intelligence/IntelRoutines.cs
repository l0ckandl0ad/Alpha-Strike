using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaStrike.Gameplay.Routines
{
    /// <summary>
    /// This class is responsible for updating calls for the Intelligence screen system backend.
    /// Updating intel reports on a regegular basis and checking for victory conditions.
    /// </summary>
    public class IntelRoutines : MonoBehaviour
    {
        private IntelData intelData;
        private int victoryPointThreshold;

        private static GameObject victoryPrefabReference;
        private static GameObject defeatPrefabReference;

        /// <summary>
        /// This is internal update delay. IntelData has ITS OWN UPDATE DELAY mechanism based on DateTime!
        /// </summary>
        private readonly float updateFrequencyInSeconds = 60;

        private void OnEnable()
        {
            intelData = GameManager.Instance.CurrentSimulation.SimulationData?.GetIntelData();
            victoryPrefabReference = Resources.Load<GameObject>("Prefabs/VictoryPopup");
            defeatPrefabReference = Resources.Load<GameObject>("Prefabs/DefeatPopup");

            if (intelData != null)
            {
                WaitForSeconds delay = new WaitForSeconds(updateFrequencyInSeconds);
                victoryPointThreshold = intelData.VictoryPointThreshold;
                StartCoroutine(IntelUpdate(intelData, delay));
            }
            else
            {
                Debug.LogError(this + ": Critical component missing!");
            }
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator IntelUpdate(IntelData intelData, WaitForSeconds waitDelay)
        {
            while (enabled)
            {
                intelData?.UpdateIntel();
                CheckForVictory(intelData);
                yield return waitDelay;
            }
        }

        /// <summary>
        /// Returns TRUE if side A's score is winning compared to side B's score.
        /// </summary>
        /// <param name="scoreSideA"></param>
        /// <param name="scoreSideB"></param>
        /// <returns></returns>
        private bool VictoryCondition(int scoreA, int scoreB, int threshold)
        {
            bool result = false;

            if (scoreA >= threshold && scoreA > scoreB && (Math.Abs(scoreA-scoreB)) > (threshold / 2))
            {
                result = true;
            }

            return result;
        }

        private void CheckForVictory(IntelData intelData)
        {  
            if (intelData.IsVictoryDeclared == false)
            {
                if (VictoryCondition(intelData.BluforScore, intelData.OpforScore, victoryPointThreshold))
                {
                    intelData.DeclareVictor(IFF.BLUFOR);
                    InitiateVictory();
                }

                if (VictoryCondition(intelData.OpforScore, intelData.BluforScore, victoryPointThreshold))
                {
                    intelData.DeclareVictor(IFF.OPFOR);
                    InitiateDefeat();
                }
            }
        }

        private void InitiateVictory()
        {
            Instantiate(victoryPrefabReference, Vector2.zero, Quaternion.identity);
        }

        private void InitiateDefeat()
        {
            Instantiate(defeatPrefabReference, Vector2.zero, Quaternion.identity);
        }

    }


}
using UnityEngine;

namespace AlphaStrike.Gameplay.Routines
{
    /// <summary>
    /// This monob should be spawned AFTER Simulation initialization!
    /// </summary>
    public class GameplayRoutines : MonoBehaviour
    {
        void Start()
        {
            gameObject.AddComponent<IntelRoutines>();
        }

    }
}
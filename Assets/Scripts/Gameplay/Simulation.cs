using UnityEngine;


namespace AlphaStrike.Gameplay
{
    public class Simulation : MonoBehaviour
    {
        private bool isInitialized = false;
        public ISimulationData SimulationData { get; private set; }

        public void Initialize(ISimulationData simulationData)
        {
            SimulationData = simulationData;
            DateTimeModel.SetDateTime(simulationData.GetDateTime());
            isInitialized = true;
        }

        private void Tick() // ONE TICK OF THE SIMULATION!
        {
            if (isInitialized) // refactor notes: Do I need this?
            {
                DateTimeModel.Tick();
                // SHOULD ANYTHING ELSE ACTUALLY BE HERE?
            }
            else
            {
                Debug.LogError(this + ": ERROR trying to run an uninitialized Simulation!");
            }
        }
        private void Update() // RUN SIMULATION DURING UPDATES
        {
            Tick();
        }

        private void OnDestroy()
        {
            Debug.Log(this + ": DESTROYED!");
        }
    }
}
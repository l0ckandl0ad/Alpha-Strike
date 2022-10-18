using System.Collections.Generic;
using UnityEngine;

namespace AlphaStrike.Gameplay.CarrierOps
{
    /// <summary>
    /// MonoBehaviour that's suppose to attach to a MapEntity (eg Fleet) that's gonna be running Carrier Ops.
    /// </summary>
    public class CarrierOpsHandler : Commandable, ICarrierOpsHandler
    {
        public List<IFlightControl> FlightControlList { get; protected set; }
        public List<ICarrierFacility> AllCarrierFacilities { get; protected set; }

        private RoutineSearchHandler routineSearchHandler;
        public IRoutineCarrierOpsSettings RoutineOpsSettings { get; protected set; }

        // DEBUG REGION START------------------------
        private bool wasRun = false;

        ICarrierCraftHoldingFacility hangarFacility;
        ICarrierCraftHoldingFacility elevatorFacility;
        ICarrierCraftHoldingFacility flightDeckFacility;

        [SerializeField] private int hangar;
        [SerializeField] private int elevator;
        [SerializeField] private int flightDeck;
        // DEBUG REGION END----------------------------

        protected override void Awake()
        {
            base.Awake();

            FlightControlList = new List<IFlightControl>();
            AllCarrierFacilities = new List<ICarrierFacility>();

            RoutineOpsSettings = new RoutineCarrierOpsSettings();
            routineSearchHandler = new RoutineSearchHandler(RoutineOpsSettings);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            MapEntity.OnStatusChange += UpdateData;
        }
        protected virtual void OnDisable()
        {
            MapEntity.OnStatusChange -= UpdateData;
        }

        /// <summary>
        /// Update data for internal usage by fetching it from associated IMapEntity.
        /// </summary>
        private void UpdateData(IMapEntity mapEntity)
        {
            FlightControlList.Clear();
            AllCarrierFacilities.Clear();
            foreach (IEntity entity in mapEntity.EntityList)
            {
                if (entity is ICarrier carrier)
                {
                    foreach (ICarrierFacility facility in carrier.CarrierFacilities)
                    {
                        if (facility.IsOperational)
                        {
                            AllCarrierFacilities.Add(facility);
                        }
                        if (facility is IFlightControl flightControl && facility.IsOperational)
                        {
                            FlightControlList.Add(flightControl);
                        }
                    }
                }
            }

            // DEBUG REGION START------------------------
            if (FlightControlList.Count > 0)
            {
                if (!wasRun && FlightControlList[0].Hangar.CarrierCraftList.Count > 1)
                {

                    hangarFacility = FlightControlList[0].Hangar;
                    elevatorFacility = FlightControlList[0].TransferArea;
                    flightDeckFacility = FlightControlList[0].FlightDeck;
                    wasRun = true;
                }
            }
            // DEBUG REGION END------------------------
        }

        public void RunRoutineSearchNow()
        {
            routineSearchHandler.SearchNow(this);
        }

        protected override void Update()
        {
            base.Update();

            if (FlightControlList.Count > 0)
            {
                routineSearchHandler.RunRoutineSearchOps(this);

                foreach (IFlightControl flightControl in FlightControlList)
                {
                    flightControl?.RunOperations(this); // run ops for each flight control in our area of responsibility
                }
            }

            // DEBUG REGION START------------------------
            if (hangarFacility != null && elevatorFacility != null && flightDeckFacility != null)
            {
                hangar = hangarFacility.CarrierCraftList.Count;
                elevator = elevatorFacility.CarrierCraftList.Count;
                flightDeck = flightDeckFacility.CarrierCraftList.Count;
            }
            // DEBUG REGION END------------------------
        }
    }
}
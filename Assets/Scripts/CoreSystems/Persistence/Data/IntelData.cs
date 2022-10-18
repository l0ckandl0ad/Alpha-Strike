using System;
using System.Collections.Generic;
using AlphaStrike.Gameplay.Entities;
using System.Linq;

[Serializable]
public class IntelData
{
    [field: NonSerialized]
    public event Action OnUpdate = delegate { };

    public bool IsVictoryDeclared { get => isVictoryDeclared; }
    public IFF Victor { get => victor; }
    public readonly int VictoryPointThreshold = 100;

    public int BluforScore { get => bluforAccumulatedPoints; }
    public int OpforScore { get => opforAccumulatedPoints; }
    public Dictionary<string, int> BluforLosses { get => bluforLosses; }
    public Dictionary<string, int> OpforLosses { get => opforLosses; }
    public Dictionary<string, int> IntelMin { get => intelMin; }
    public Dictionary<string, int> IntelMax { get => intelMax; }

    /// <summary>
    /// Intel report error in percent, where 1 = 100%, 0.5 = 50% etc.
    /// </summary>
    public float IntelError { get => intelError; }
    public DateTime IntelLastUpdate { get => intelLastUpdate; }
    public DateTime IntelExpirationDate { get => intelExpirationDate; }

    private bool isVictoryDeclared = false;
    private IFF victor = IFF.EMPTY;

    private int bluforAccumulatedPoints;
    private int opforAccumulatedPoints;

    private Dictionary<string, int> bluforLosses;
    private Dictionary<string, int> opforLosses;

    private double intelUpdateFrequencyInHours = 6; // TEMP! change to 1-6d when done!
    private DateTime intelLastUpdate;
    private DateTime intelExpirationDate;

    private Dictionary<string, int> intelBase;
    private Dictionary<string, int> intelMin;
    private Dictionary<string, int> intelMax;
    private float intelError = 0.95f; // 0.5f = 50%
    /// <summary>
    /// Minimum/Bottomline accuracy of intel reports.
    /// </summary>
    private float intelErrorMin = 0.5f; // 0.5f = 50%
    /// <summary>
    /// How much intel improves with each update.
    /// </summary>
    private float intelImprovementIncrement = 0.05f;

    public IntelData()
    {
        Initialize();
    }

    public void ReportDestroyed(ISpacePlatform platformDestroyed)
    {
        if (!platformDestroyed.IsAlive)
        {
            switch (platformDestroyed.IFF)
            {
                case IFF.BLUFOR:
                    AddPointsToOpfor(platformDestroyed.VPCost);
                    RecordPlatform(platformDestroyed, bluforLosses);
                    break;
                case IFF.OPFOR:
                    AddPointsToBlufor(platformDestroyed.VPCost);
                    RecordPlatform(platformDestroyed, opforLosses);
                    break;
                default:
                    // do nothing
                    break;
            }
            OnUpdate?.Invoke();
        }
    }

    public void DeclareVictor(IFF victor)
    {
        if (isVictoryDeclared == false)
        {
            isVictoryDeclared = true;
            this.victor = victor;
        }
    }

    private void Initialize()
    {
        bluforLosses = new Dictionary<string, int>();
        opforLosses = new Dictionary<string, int>();

        intelBase = new Dictionary<string, int>();
        intelMin = new Dictionary<string, int>();
        intelMax = new Dictionary<string, int>();

        foreach (string s in Enum.GetNames(typeof(SpacePlatformType)))
        {
            bluforLosses.Add(s, 0);
            opforLosses.Add(s, 0);
        }
        intelExpirationDate = DateTimeModel.CurrentDateTime;
        FlushIntelData();
    }

    private void FlushIntelData()
    {
        intelBase.Clear();
        intelMin.Clear();
        intelMax.Clear();

        foreach (string s in Enum.GetNames(typeof(SpacePlatformType)))
        {
            intelBase.Add(s, 0);
            intelMin.Add(s, 0);
            intelMax.Add(s, 0);
        }
    }

    private void AddPointsToBlufor(int points)
    {
        bluforAccumulatedPoints += points;
    }
    private void AddPointsToOpfor(int points)
    {
        opforAccumulatedPoints += points;
    }

    private void RecordPlatform(ISpacePlatform platformDestroyed, Dictionary<string, int> lostPlatforms)
    {
        string type = platformDestroyed.PlatformType.ToString();

        if (lostPlatforms.ContainsKey(type))
        {
            lostPlatforms[type]++;
        }
        else
        {
            lostPlatforms.Add(type, 1);
        }
    }

    public void UpdateIntel()
    {
        if (DateTimeModel.CurrentDateTime >= intelExpirationDate)
        {
            List<ISpacePlatform> platforms = FindAllPlatforms(GameSettings.EnemySide);

            FlushIntelData();

            if (platforms == null || platforms.Count == 0) return;

            foreach (ISpacePlatform platform in platforms)
            {
                RecordPlatform(platform, intelBase);
            }

            if (intelError > intelErrorMin)
            {
                intelError -= intelImprovementIncrement;// improve intel accuracy with each update
            }

            foreach (KeyValuePair<string, int> entry in intelBase)
            {
                float rndMinModifier = UnityEngine.Random.Range(1f - intelError, 1f);
                float rndMaxModifier = UnityEngine.Random.Range(1f, 1f + intelError);

                float value = entry.Value;
                float minValue = value * rndMinModifier;
                float maxValue = value * rndMaxModifier;
                intelMin[entry.Key] = (int)minValue;
                intelMax[entry.Key] = (int)maxValue;
            }

            intelExpirationDate = DateTimeModel.CurrentDateTime.AddHours(intelUpdateFrequencyInHours);
            intelLastUpdate = DateTimeModel.CurrentDateTime;

            OnUpdate?.Invoke();

            MessageLog.SendMessage(new Message("Intelligence report updated!", MessagePrecedence.IMMEDIATE));
        }
    }

    private List<ISpacePlatform> FindAllPlatforms(IFF iff)
    {
        List<ISpacePlatform> platforms = new List<ISpacePlatform>();
        
        Fleet[] fleets = UnityEngine.Object.FindObjectsOfType<Fleet>();

        foreach (Fleet fleet in fleets)
        {
            if (fleet.IFF == iff)
            {
                platforms.AddRange(EntityUtils.EntityToSpacePlatformList(fleet.EntityList));
            }
        }

        List<ISpacePlatform> sortedPlatforms = platforms.OrderBy(x => x.IFF == iff).ToList();

        foreach (ISpacePlatform platform in sortedPlatforms)
        {
            if (platform is ICarrier carrier)
            {
                foreach (ICarrierCraftHoldingFacility facility in carrier.CarrierFacilities)
                {
                    sortedPlatforms.AddRange(facility.CarrierCraftList);
                }
            }
        }

        return sortedPlatforms;
    }
}
using System;

public class RoutineCarrierOpsSettings : IRoutineCarrierOpsSettings
{
    private float[,] searchSectors;
    private bool[] sectorToggles;

    public RoutineCarrierOpsSettings()
    {
        Initialize();
    }

    public float SearchFrequencyInMinutes { get; private set; } = 0f;   // TEMP for debug!
    public float Range { get; private set; } = 500f;    // TEMP for debug!

    public bool ZeroDegrees { get; private set; }
    public bool FortyFiveDegrees { get; private set; }
    public bool NinetyDegrees { get; private set; }
    public bool OneThirtyFiveDegrees { get; private set; }

    public bool OneEightyDegrees { get; private set; }
    public bool TwoTwentyFiveDegrees { get; private set; }
    public bool TwoSeventyDegrees { get; private set; }
    public bool ThreeFifteenDegrees { get; private set; }

    public bool Shadow { get; private set; }

    private void Initialize()
    {
        PopulateSearchVectors();
    }

    private void PopulateSearchVectors() // populate an array of sector data with specific angles for each sector
    {
        searchSectors = new float[32, 2]; // this is a 4 angles per sector setup, total: 32 directions for 360 degrees

        float value = 331.875f;
        for (int i = 0; i < 32; i++)
        {
            value += 11.25f;
            if (value >= 360f)
            {
                value -= 360f;
            }
            searchSectors[i, 1] = value;
        }
    }
    private void ReadSectorToggles()
    {
        sectorToggles = new bool[]{ ZeroDegrees, FortyFiveDegrees, NinetyDegrees, OneThirtyFiveDegrees,
            OneEightyDegrees, TwoTwentyFiveDegrees, TwoSeventyDegrees, ThreeFifteenDegrees };
    }

    public float[,] GetSearchSectors()
    {
        ReadSectorToggles(); // read current state of search settings for what sectors to enable search for

        int sectorIterator = 0;
        foreach (bool sector in sectorToggles)
        {
            for (int i=0; i<4; i++) // populate array with data four times for each sector
            {
                searchSectors[sectorIterator, 0] = Convert.ToSingle(sector);
                sectorIterator++;
            }
        }
        return searchSectors;
    }

    public void SetRange(float range)
    {
        Range = range;
    }
    public void SetSearchFrequencyInMinutes(float searchFrequencyInMinutes)
    {
        SearchFrequencyInMinutes = searchFrequencyInMinutes;
    }

    public void SetZeroDegrees(bool value)
    {
        ZeroDegrees = value;
    }
    public void SetFortyFiveDegrees(bool value)
    {
        FortyFiveDegrees = value;
    }
    public void SetNinetyDegrees(bool value)
    {
        NinetyDegrees = value;
    }
    public void SetOneThirtyFiveDegrees(bool value)
    {
        OneThirtyFiveDegrees = value;
    }
    public void SetOneEightyDegrees(bool value)
    {
        OneEightyDegrees = value;
    }
    public void SetTwoTwentyFiveDegrees(bool value)
    {
        TwoTwentyFiveDegrees = value;
    }
    public void SetTwoSeventyDegrees(bool value)
    {
        TwoSeventyDegrees = value;
    }
    public void SetThreeFifteenDegrees(bool value)
    {
        ThreeFifteenDegrees = value;
    }
    public void SetShadow(bool value)
    {
        Shadow = value;
    }
}

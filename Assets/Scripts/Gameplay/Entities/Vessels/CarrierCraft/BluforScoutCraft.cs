using UnityEngine;

[System.Serializable]
public class BluforScoutCraft : CarrierCraft
{
    private static string hullClass = "VS";
    private static int hullNumber;
    public BluforScoutCraft()
    {
        IFF = IFF.BLUFOR;
        MinSize = 2;
        MaxSize = 5;
        Structure = 8;
        VPCost = 1;
        PlatformType = SpacePlatformType.SCOUT;

        Class = "S-1 Cat Eye";
        ++hullNumber;
        HullName = hullClass + "-" + hullNumber.ToString();
        Name = HullName;
        MaxSpeed = 80;

        IsAllowedForSearchOps = true;
    }
}

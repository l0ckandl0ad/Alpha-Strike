using UnityEngine;

[System.Serializable]
public class BluforFighter : CarrierCraft
{
    private static string hullClass = "VF";
    private static int hullNumber;
    public BluforFighter()
    {
        IFF = IFF.BLUFOR;
        MinSize = 4;
        MaxSize = 10;
        Structure = 10;
        VPCost = 1;
        PlatformType = SpacePlatformType.FTR;

        Class = "F-1 Lightning";
        ++hullNumber;
        HullName = hullClass + "-" + hullNumber.ToString();
        Name = HullName;
        MaxSpeed = 120;

        Weapons.Add(new LightLaser());
    }
}

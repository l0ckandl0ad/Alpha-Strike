[System.Serializable]
public class OpforCA : CombatVessel
{
    private static string hullClass = "CA";
    private static int hullNumber;
    public OpforCA(string name)
    {
        Name = name;
        IFF = IFF.OPFOR;
        MinSize = 46;
        MaxSize = 380;
        Structure = 800;
        VPCost = 80;
        PlatformType = SpacePlatformType.CA;

        Class = "Event Horizon";
        ++hullNumber;
        HullName = hullClass + "-" + hullNumber.ToString();
        MaxSpeed = 30;

        Weapons.Add(new HeavyLaser());
        Weapons.Add(new HeavyLaser());
        Weapons.Add(new HeavyLaser());
    }

}

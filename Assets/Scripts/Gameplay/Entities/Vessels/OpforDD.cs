[System.Serializable]
public class OpforDD : CombatVessel
{
    private static string hullClass = "DD";
    private static int hullNumber;
    public OpforDD(string name)
    {
        Name = name;
        IFF = IFF.OPFOR;
        MinSize = 26;
        MaxSize = 210;
        Structure = 180;
        VPCost = 20;
        PlatformType = SpacePlatformType.DD;

        Class = "Desolator";
        ++hullNumber;
        HullName = hullClass + "-" + hullNumber.ToString();
        MaxSpeed = 40;

        Weapons.Add(new Laser());
        Weapons.Add(new Laser());
    }

}

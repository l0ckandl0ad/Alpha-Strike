[System.Serializable]
public class BluforDD : CombatVessel
{
    private static string hullClass = "DD";
    private static int hullNumber;
    public BluforDD(string name)
    {
        Name = name;
        IFF = IFF.BLUFOR;
        MinSize = 28;
        MaxSize = 190;
        Structure = 200;
        VPCost = 20;
        PlatformType = SpacePlatformType.DD;

        Class = "Vigilant";
        ++hullNumber;
        HullName = hullClass + "-" + hullNumber.ToString();
        MaxSpeed = 38;

        Weapons.Add(new Laser());
        Weapons.Add(new Laser());
    }
}

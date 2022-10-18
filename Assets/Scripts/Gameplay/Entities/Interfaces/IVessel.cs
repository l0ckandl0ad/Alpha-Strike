/// <summary>
/// Mobile entity that can move and be targeted in combat.
/// </summary>
public interface IVessel : ISpacePlatform, ITargetable
{
    string Class { get; } // a name of the class, e.g. "Yorktown"
    string HullName { get; } // full hull name that combines Hull Class with Hull Number. is there a better name for this?
    float MaxSpeed { get; }

    //float MaxEndurance { get; }
    //float Endurance { get; } // current endurance left
}

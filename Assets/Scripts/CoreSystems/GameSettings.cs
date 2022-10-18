public static class GameSettings
{
    public static IFF PlayerSide { get; private set; } = IFF.BLUFOR;
    public static IFF EnemySide { get => GetOpponentSide(PlayerSide); }

    public static void SetPlayerSide (IFF side)
    {
        PlayerSide = side;
    }
    public static IFF GetOpponentSide(IFF side) // this logic is flawed because it means there's only ONE enemy
    {
        switch(side)
      {
            case IFF.BLUFOR:
                return IFF.OPFOR;
            case IFF.OPFOR:
                return IFF.BLUFOR;
            default:
                return IFF.OPFOR;
        }
    }

}

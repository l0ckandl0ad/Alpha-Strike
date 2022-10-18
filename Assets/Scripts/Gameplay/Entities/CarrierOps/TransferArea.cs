[System.Serializable]
public class TransferArea : Hangar
{
    public TransferArea()
    {
        Name = "Transfer Area";
        Type = ModuleType.TransferArea;
        CanReadyCraft = false;
        MaxNumberOfCraftAllowed = 8;
    }
}

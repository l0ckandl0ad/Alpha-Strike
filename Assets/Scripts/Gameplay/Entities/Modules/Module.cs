[System.Serializable]
public abstract class Module : IModule
{
    public string Name { get; protected set; } = "Base Module";
    public float Health { get; protected set; } = 1f;   // 1 = 100%, 0.5 = 50%
    public bool IsOperational { get; protected set; } = true;
    public ModuleType Type { get; protected set; } = ModuleType.Base;

    public virtual void Disable()
    {
        IsOperational = false;
    }
    public virtual void Enable()
    {
        IsOperational = true;
    }
}

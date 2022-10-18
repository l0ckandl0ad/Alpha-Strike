public interface IModule
{
    string Name { get; }
    float Health { get; }
    bool IsOperational { get; }
    ModuleType Type { get; }
    void Enable();
    void Disable();
}
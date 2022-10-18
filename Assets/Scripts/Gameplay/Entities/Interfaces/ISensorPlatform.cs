using System.Collections.Generic;
public interface ISensorPlatform : ISpacePlatform
{
    List<IModule> Sensors { get; }
}
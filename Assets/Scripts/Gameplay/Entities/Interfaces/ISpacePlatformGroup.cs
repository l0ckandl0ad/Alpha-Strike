using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpacePlatformGroup : IMapEntity
{

    /// <summary>
    /// When one of the entities in this platform group is hit, the event will be called.
    /// </summary>
    event Action<ISpacePlatform> OnHit;
    /// <summary>
    /// When one of the entities in this platform group is destroyed, the event will be called.
    /// </summary>
    event Action<ISpacePlatform> OnDestroyed;
    List<ISpacePlatform> GetSpacePlatforms();
}

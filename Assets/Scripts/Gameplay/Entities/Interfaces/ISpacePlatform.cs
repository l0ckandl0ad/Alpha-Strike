using System;
/// <summary>
/// A physical entity that can be targeted. Movement not included!
/// </summary>
public interface ISpacePlatform : IEntity, ITargetable
{
    /// <summary>
    /// How much VPs would the opponent score by destroying this platform?
    /// </summary>
    int VPCost { get; }

    SpacePlatformType PlatformType { get; }

    event Action<ISpacePlatform> OnSpacePlatformHit;
    event Action<ISpacePlatform> OnSpacePlatformDestroyed;
}

using UnityEngine;
/// <summary>
/// To be attached to a GameObject with script implementing IMapEntity
/// </summary>
public interface IDetectable
{
    IFF IFF { get; }
    IMapEntity MapEntity { get; }
    CircleCollider2D DetectableTriggerCollider2D { get; }
    bool IsDetectedByEnemy { get; }
    void Reveal();
}

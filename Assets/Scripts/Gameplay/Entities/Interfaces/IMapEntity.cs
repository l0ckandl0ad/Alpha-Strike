using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Represents an object on game map that could be selected by the player (fleet, astro object etc).
/// </summary>
public interface IMapEntity
{
    string Name { get; }
    IFF IFF { get; }
    Transform Transform { get; }
    GameObject GameObject { get; }
    /// <summary>
    /// A list of entities that are part of this MapEntity. Even if it's just one entity, it's gotta be there.
    /// </summary>
    List<IEntity> EntityList { get; }
    void AddEntity(IEntity entity);
    void AddEntities(List<IEntity> entities);
    void RemoveEntity(IEntity entity);
    void RemoveEntities(List<IEntity> entities);
    /// <summary>
    /// Event allows outsiders to check in when somethin's changed on this MapEntity.
    /// </summary>
    event Action<IMapEntity> OnStatusChange;
    string EntityListDisplay(); // temp
    bool IsVisible { get; } // part of Fog of War system (iteration 1) .. refactor to a separate interface!
    void MakeVisible(bool trueOrFalse); // part of Fog of War system (iteration 1).. refactor to a separate interface!
}
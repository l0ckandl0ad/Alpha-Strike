using System;
using System.Collections.Generic;
using UnityEngine;

public class EmptyMapEntity : IMapEntity
{
    public string Name { get; }
    public IFF IFF { get; } = IFF.EMPTY;
    public GameObject GameObject { get; } = null;
    public Transform Transform { get; } = null;
    public bool IsVisible { get; } = false;
    public List<IEntity> EntityList { get; set; }
    public event Action<IMapEntity> OnStatusChange = delegate { };
    public void MakeVisible(bool trueOrFalse)
    {
        //IsVisible = trueOrFalse; // not really needed?
    }
    public string EntityListDisplay()
    {
        string displayText = "";
        // do nothing
        return displayText;
    }

    public void AddEntity(IEntity entity)
    {
        // ....
    }
    public void AddEntities(List<IEntity> entities)
    {
        //
    }
    public void RemoveEntity(IEntity entity)
    {
        // ....
    }

    public void RemoveEntities(List<IEntity> entities)
    {
        // nothing here
    }
}

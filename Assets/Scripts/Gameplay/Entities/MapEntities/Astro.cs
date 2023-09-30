using System;
using System.Collections.Generic;
using UnityEngine;

public class Astro : MonoBehaviour, IMapEntity
{
    public string SayMyName; // temp - should be refactored with proper prefab and astro object types
    public string Name { get; private set; }
    public IFF IFF { get; private set; } = IFF.ASTRO;
    public GameObject GameObject { get => gameObject; }
    public Transform Transform { get => gameObject.transform; }
    public bool IsVisible { get; private set; } = true;
    public List<IEntity> EntityList { get; set; }
    public event Action<IMapEntity> OnStatusChange = delegate { };
    public string EntityListDisplay() // temp
    {
        return Name + " is an astronomical object.";
    }

    public void MakeVisible(bool trueOrFalse)
    {
        IsVisible = trueOrFalse;
    }

    private void Start() // temp
    {
        Name = SayMyName;
    }

    public void AddEntity(IEntity entity)
    {
        // ....
    }
    public void AddEntities(List<IEntity> entities)
    {
        //...
    }
    public void RemoveEntity(IEntity entity)
    {
        //
    }
    public void RemoveEntities(List<IEntity> entities)
    {
        //....
    }
}


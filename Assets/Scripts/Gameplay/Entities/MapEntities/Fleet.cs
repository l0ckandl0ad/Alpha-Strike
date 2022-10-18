using AlphaStrike.Gameplay.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Fleet : MonoBehaviour, IMapEntity, IMovable, ISpacePlatformGroup
{
    public string Name { get; private set; } = "Fleet";
    public IFF IFF { get; private set; } = IFF.BLUFOR;
    public GameObject GameObject { get => gameObject; }
    public Transform Transform { get => gameObject.transform; }
    public bool IsVisible { get; private set; } = false;
    public float Speed { get; private set; } = 0f;
    public List<IEntity> EntityList { get; private set; } = new List<IEntity>();
    public event Action<IMapEntity> OnStatusChange = delegate { };
    public event Action<ISpacePlatform> OnHit = delegate { };
    public event Action<ISpacePlatform> OnDestroyed = delegate { };

    private bool isBeingDestroyed = false;
    private IntelData intelData;

    private void Start()
    {
        intelData = GameManager.Instance.CurrentSimulation.SimulationData?.GetIntelData();
        if (intelData == null) Debug.LogError(this + ": Critical component not found!");
    }

    private void OnEnable()
    {
        OnStatusChange += OnStatusChanged;
    }
    private void OnDisable()
    {
        OnStatusChange -= OnStatusChanged;
    }

    public void SetName(string newName)
    {
        Name = newName;
        if (gameObject != null)
        {
            gameObject.name = newName;
        }
    }
    public void SetIFF(IFF newIFF)
    {
        IFF = newIFF;
        SetIconColor(newIFF);
        if (newIFF == GameSettings.PlayerSide)
        {
            MakeVisible(true);
        }
        else
        {
            MakeVisible(false);
        }
        OnStatusChange?.Invoke(this);
    }

    public List<ISpacePlatform> GetSpacePlatforms()
    {
        List<ISpacePlatform> platforms = null;

        if (EntityList.Count > 0)
        {
            platforms = EntityUtils.EntityToSpacePlatformList(EntityList);
            platforms = platforms.OrderByDescending(platform => platform.VPCost).ToList();
        }
        return platforms;
    }

    public void MakeVisible(bool trueOrFalse)
    {
        IsVisible = trueOrFalse;
    }
    public void AddEntity(IEntity entity)
    {
        if (entity.IsAlive)
        {
            EntityList.Add(entity);
            entity.BindToMapEntity(this);
            entity.OnStatusChange += OnEntityStatusChanged;
            
            if (entity is ISpacePlatform spacePlatform)
            {
                spacePlatform.OnSpacePlatformHit += OnSpacePlatformHit;
                spacePlatform.OnSpacePlatformDestroyed += OnSpacePlatformDestroyed;
            }

            OnStatusChange?.Invoke(this);
        }
    }
    public void AddEntities(List<IEntity> newEntities)
    {
        foreach (IEntity entity in newEntities)
        {
            AddEntity(entity);
        }
    }
    public void RemoveEntity(IEntity entity)
    {
        EntityList.Remove(entity);
        entity.BindToMapEntity(null);
        entity.OnStatusChange -= OnEntityStatusChanged;

        if (entity is ISpacePlatform spacePlatform)
        {
            spacePlatform.OnSpacePlatformHit -= OnSpacePlatformHit;
            spacePlatform.OnSpacePlatformDestroyed -= OnSpacePlatformDestroyed;
        }

        OnStatusChange?.Invoke(this);
    }
    public void RemoveEntities(List<IEntity> entities)
    {
        foreach (IEntity entity in entities)
        {
            RemoveEntity(entity);
        }
    }

    private void SetIconColor(IFF iff)
    {
        // refactor note - move this method outta here, triggering it via OnStatusChange in another script
        Image mapEntityIcon = GetComponentInChildren<Image>();

        if (mapEntityIcon != null)
        {
            switch (iff)
            {
                case IFF.BLUFOR:
                    mapEntityIcon.color = Color.blue;
                    break;
                case IFF.OPFOR:
                    mapEntityIcon.color = Color.red;
                    break;
                default:
                    mapEntityIcon.color = Color.grey;
                    break;
            }
        }
        else
        {
            Debug.LogError(this + ": Cannot set icon color according to IFF. No Image component found.");
        }
    }

    public string EntityListDisplay() // TEMP! Refactor this shit! THERE'S NOTHING MORE PERMANENT THEN TEMPORARY, HUH?!
    {
        string displayText = ""; //  "\n";

        if (EntityList != null & EntityList.Count >= 1)
        {
            displayText = "Composition (" + EntityList.Count.ToString() + " units):";
            foreach (IEntity ship in EntityList)
            {
                displayText += "\n"+ ship.Name;
            }
            displayText += "\nSpeed: " + Speed.ToString();
        }
        return displayText;
    }

    private void OnStatusChanged(IMapEntity mapEntity)
    {
        //MapEntityUtils.RemoveDeadEntitiesFromMapEntity(this); // temp remove this to check if it's working okay
        Speed = MapEntityUtils.GetMaxSpeed(this);
        DestroyThisFleetAtRuntimeIfEmpty();
    }
    private void OnEntityStatusChanged()
    {
        OnStatusChange?.Invoke(this);// when something changes on registered entity, the whole fleet status changes
                                    // thus we must envoke the event of fleet's status change
    }
    private void OnSpacePlatformHit(ISpacePlatform spacePlatform)
    {
        OnHit?.Invoke(spacePlatform);
        OnStatusChange?.Invoke(this);
    }
    private void OnSpacePlatformDestroyed(ISpacePlatform spaceplatform)
    {
        OnDestroyed?.Invoke(spaceplatform);
        intelData.ReportDestroyed(spaceplatform);
        RemoveEntity(spaceplatform);
        OnStatusChange?.Invoke(this);
    }
    private void DestroyThisFleetAtRuntimeIfEmpty() // refactor notes: move me outta here to MapEntityUtils.cs!
    {
        if (!DateTimeModel.IsPaused && EntityList.Count == 0 && !isBeingDestroyed)
        {
            StartCoroutine(DestroyThisFleet());
        }
    }
    private IEnumerator DestroyThisFleet()
    {
        float destructionTimer = 5f;
        yield return new WaitForSeconds(0.1f); // let's wait a bit before actually destroying the fleet...
        if (EntityList.Count > 0) yield break; // stop self-destruction of the fleet
        if (!isBeingDestroyed)
        {
            isBeingDestroyed = true;
            yield return new WaitForSeconds(destructionTimer); // wait to allow ongoing fx to play (eg explosions)
            yield return new WaitForEndOfFrame();
            MessageLog.SendMessage("FLEET " + Name + " DISBANDS", MessagePrecedence.FLASH);
            Destroy(gameObject);
        }
    }
}
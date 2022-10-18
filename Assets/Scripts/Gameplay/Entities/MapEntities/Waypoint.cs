using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour, IMapEntity, IDragHandler
{
    private IMapEntity parentMapEntity;

    public string Name { get; private set; } = "Waypoint";
    public IFF IFF { get; private set; } = IFF.EMPTY;
    public GameObject GameObject { get => gameObject; }
    public Transform Transform { get => gameObject.transform; }
    public List<IEntity> EntityList { get; set; }
    public event Action<IMapEntity> OnStatusChange = delegate { };
    public bool IsVisible { get; private set; } = true;

    private IWaypointOwner waypointOwner;
    private Canvas canvas;
    private new Camera camera;
    private Text waypointTextComponent;

    private void Awake()
    {
        camera = Camera.main;
        canvas = GetComponentInChildren<Canvas>();
        waypointTextComponent = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        if (parentMapEntity == null || canvas == null || camera == null || waypointOwner == null)
        {
            Debug.LogError(this + ": critical components not found!");
        }
    }
    public void Initialize(IMapEntity parentMapEntity, IWaypointOwner waypointOwner)
    {
        this.parentMapEntity = parentMapEntity;
        IFF = parentMapEntity.IFF;
        this.waypointOwner = waypointOwner;
        Vector2 position = transform.position;
        SetWaypointText(position.ToString());
        UpdateView(MapEntitySelectionModel.SelectedMapEntity);
    }
    public string EntityListDisplay()
    {
        return "This is a Waypoint.";
    }
    public void MakeVisible(bool trueOrFalse)
    {
        IsVisible = trueOrFalse;
    }

    private void UpdateView(IMapEntity selectedMapEntity)
    {
        // Display only if binded to currently selected MapEntity and if it is friendly
        if (selectedMapEntity != null && selectedMapEntity == parentMapEntity && IFF == GameSettings.PlayerSide)
        {
            MakeVisible(true);
        }
        else
        {
            MakeVisible(false);
        }
    }

    private void OnEnable()
    {
        MapEntitySelectionModel.OnMapEntitySelection += UpdateView;
    }

    private void OnDisable()
    {
        MapEntitySelectionModel.OnMapEntitySelection -= UpdateView;
    }

    public void AddEntity(IEntity entity)
    {
        // ...
    }
    public void AddEntities(List<IEntity> entities)
    {
        //
    }
    public void RemoveEntity(IEntity entity)
    {
        // ugh
    }
    public void RemoveEntities(List<IEntity> entities)
    {
        //....
    }

    public void OnDrag(PointerEventData eventData)
    {
        // we're using vector2 here because camera.pos.z is at -10 and we need everything to be at pos.z = 0
        Vector2 newPos = camera.ScreenToWorldPoint(eventData.position); //maybe use eventData.delta instead? 
        Transform.position = newPos;
        waypointOwner.UpdateDestination(newPos);
        SetWaypointText(newPos.ToString());
    }

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
        SetWaypointText(newPosition.ToString());
    }

    public void SetWaypointText(string text)
    {
        waypointTextComponent.text = text;
    }

}
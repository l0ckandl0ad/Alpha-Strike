using UnityEngine;
using UnityEngine.InputSystem;

public class SelectedMapEntityUIHandler : MonoBehaviour
{
    private IMapEntity currentMapEntity;
    private MapEntityClickSelector mapEntityClickSelector;
    private Camera mainCamera;
    private Mouse mouse;

    [Header("PANELS")]
    [SerializeField] private ActivePlatformGroupPanel ActivePlatformGroupPanel;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        mainCamera = Camera.main;
        mouse = Mouse.current;

        MapEntitySelectionModel.Initialize();

        if (mapEntityClickSelector == null)
        {
            mapEntityClickSelector = new MapEntityClickSelector();
            mapEntityClickSelector.SelectEmpty();
        }

        MapEntitySelectionModel.OnMapEntitySelection += UpdateView; // subscribe to selection event
    }
    private void UnsubscribeFromCurrentEvents()
    {
        MapEntitySelectionModel.OnMapEntitySelection -= UpdateView; // unsubscribe from selection event
        if (currentMapEntity != null)
        {
            currentMapEntity.OnStatusChange -= UpdateView;
        }
    }

    void OnDisable()
    {
        UnsubscribeFromCurrentEvents();
    }
    private void OnDestroy()
    {
        UnsubscribeFromCurrentEvents();
    }

    void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame) // refactor linking it to the entire input system
        {
            SelectOnClick();
        }
    }

    private void SelectOnClick()
    {
        mapEntityClickSelector.SelectMapEntitiesOnClick(mainCamera.ScreenToWorldPoint(mouse.position.ReadValue()));
    }

    private void OnEntitySelection(IMapEntity newMapEntity)
    {
        if (currentMapEntity != null)
        {
            currentMapEntity.OnStatusChange -= UpdateView; // unsub from previous entity events
        }
        currentMapEntity = newMapEntity;
        currentMapEntity.OnStatusChange += UpdateView; // subscribe to events happening on the entity
    }

    private void UpdateView(IMapEntity selected) // Update UI to display info of the selected object
    {
        if (ActivePlatformGroupPanel && selected != null)
        {
            OnEntitySelection(selected);

            if (selected is ISpacePlatformGroup platformGroup && platformGroup.IsVisible &&
                platformGroup.IFF == IFF.BLUFOR)
            {
                ActivePlatformGroupPanel.Show(platformGroup);
            }
            else
            {
                ActivePlatformGroupPanel.Hide();
            }
        }
    }
}
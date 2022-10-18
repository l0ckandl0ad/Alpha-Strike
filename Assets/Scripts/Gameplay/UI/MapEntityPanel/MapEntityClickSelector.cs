using UnityEngine;
using UnityEngine.EventSystems;

public class MapEntityClickSelector
{
    private readonly EmptyMapEntity emptySelection = new EmptyMapEntity();   //dummy IMapEntity
    private IMapEntity previouslySelectedMapEntity = null;

    public void SelectMapEntitiesOnClick(Vector3 worldPosition)
    {
        IMapEntity[] mapEntities = GetVisibleMapEntitiesAtPosition(worldPosition);
        if (mapEntities != null)
        {
            switch (mapEntities.Length) // How many intities are there?
            {
                case 0:
                    SelectEmpty(); // None? Deselect!
                    break;
                case 1:
                    SelectMapEntityFromArray(mapEntities, 0); // One? Select first entity in array
                    break;
                default:
                    HandleMultipleMapEntitiesSelection(mapEntities); // Many? Figure which one to select
                    break;
            }
        }
    }

    private IMapEntity[] GetVisibleMapEntitiesAtPosition(Vector3 worldPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(worldPosition);
        IMapEntity[] mapEntities = new IMapEntity[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.TryGetComponent(out IMapEntity mapEntity))
            {
                if (mapEntity.IsVisible)
                {
                    mapEntities[i] = mapEntity;
                }
            }
        }
        return mapEntities;

    }
    public void SelectEmpty()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // checking if there's UI in the way
        {
            MapEntitySelectionModel.SelectMapEntity(emptySelection);
            previouslySelectedMapEntity = null;
        }
    }
    private void SelectMapEntityFromArray(IMapEntity[] visibleMapEntities, int index)
    {
        if (visibleMapEntities[index] != null)
        {
            MapEntitySelectionModel.SelectMapEntity(visibleMapEntities[index]);
            previouslySelectedMapEntity = visibleMapEntities[index];
        }
    }

    private void HandleMultipleMapEntitiesSelection(IMapEntity[] visibleMapEntities)
    {
        if (visibleMapEntities.Length >= 2)
        {
            if (previouslySelectedMapEntity == null) // select first if none were selected before
            {
                SelectMapEntityFromArray(visibleMapEntities, 0);
            }
            else // or...
            {
                int index = 0;
                bool selected = false;
                foreach (IMapEntity c in visibleMapEntities) // cycle through colliders
                {
                    if (c == previouslySelectedMapEntity) // to find a match with previously selected
                    {
                        if (index + 1 < visibleMapEntities.Length) // and select next one unless it's at the end
                        {
                            SelectMapEntityFromArray(visibleMapEntities, index + 1);
                            selected = true;
                            break;
                        }
                        else
                        {
                            SelectMapEntityFromArray(visibleMapEntities, 0); // if at the end, select the first one
                            selected = true;
                            break;
                        }
                    }
                    index++;
                }
                if (!selected) SelectMapEntityFromArray(visibleMapEntities, 0); // if no match found, get the first
            }
        }
    }
}

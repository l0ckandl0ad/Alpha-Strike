using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls position and visibility (Fog of War v1) of the IMapEntity icon.
/// </summary>
/// <remarks>
/// Should be attached to GameObject that contains IMapEntity (eg Fleet, Waypoint) that has child object with
/// Image component (icon).
/// </remarks>
public class UIMapEntityIconView : MonoBehaviour
{
    private Camera mainCamera;
    private IMapEntity mapEntity;
    private Image mapEntityIcon;
    private GameObject mapEntityIconGameObject;
    private Transform mapEntityIconTransform;
    private Vector3 mapEntityIconScreenSpacePosition;

    private void Awake()
    {
        CacheReferences();
    }

    void OnEnable()
    {
        CacheReferences();
    }

    private void CacheReferences()
    {
        mainCamera = Camera.main;
        mapEntity = GetComponent<IMapEntity>();
        mapEntityIcon = GetComponentInChildren<Image>();
        mapEntityIconGameObject = mapEntityIcon.gameObject;
        if (mapEntityIcon != null)
        {
            mapEntityIconTransform = mapEntityIcon.gameObject.transform;
        }
    }
    private void MakeVisible(bool trueOrFalse)
    {
        mapEntityIconGameObject.SetActive(trueOrFalse);
    }
    private void LateUpdate()
    {
        if (mapEntity.IsVisible)
        {
            MakeVisible(true);
            AdjustIconPositionFromWorldToScreenSpace();
        }
        else
        {
            MakeVisible(false);
        }
    }

    private void AdjustIconPositionFromWorldToScreenSpace()
    {
        if (mapEntity != null)
        {
            mapEntityIconScreenSpacePosition = mainCamera.WorldToScreenPoint(transform.position);
            if (transform.position != mapEntityIconScreenSpacePosition)
            {
                mapEntityIconTransform.position = mapEntityIconScreenSpacePosition;
            }
        }
    }
}

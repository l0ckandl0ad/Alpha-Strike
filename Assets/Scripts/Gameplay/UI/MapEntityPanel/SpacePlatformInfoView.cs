using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpacePlatformInfoView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Text platformName;
    [SerializeField] Text type;
    [SerializeField] Text structure;

    ISpacePlatform platform;

    public void Initialize(ISpacePlatform spacePlatform)
    {
        if (spacePlatform == null) return;

        platform = spacePlatform;

        platformName.text = spacePlatform.Name;
        SetType(spacePlatform.PlatformType);
        structure.text = spacePlatform.Structure.ToString();

        gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(platform.Name);
        // implement popup later
    }

    private void SetType(SpacePlatformType type)
    {
        this.type.text = type.ToString();
    }
}

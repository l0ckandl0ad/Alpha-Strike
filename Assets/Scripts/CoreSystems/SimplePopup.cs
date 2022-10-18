using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimplePopup : MonoBehaviour, IDragHandler
{
    /// <summary>
    /// Highest object in the hierarchy of objects displaying the popup that can be moved,
    /// eg the panel the everything is on.
    /// </summary>
    [SerializeField] private GameObject visualsTopParent;
    [SerializeField] private Button closeButton;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 posDelta = eventData.delta;
        visualsTopParent.transform.position += posDelta;
    }

    private void Start()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => Close());
        }
        else
        {
            Debug.LogError(this + ": Critical component missing!");
        }
    }

    private void OnEnable()
    {
        DateTimeModel.Pause();
    }

    private void Close()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
    }
}

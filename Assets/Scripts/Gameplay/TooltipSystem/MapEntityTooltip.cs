using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapEntityTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private IMapEntity owner;
    private string tooltipText;


    private void Start()
    {
        owner = transform.root.GetComponentInChildren<IMapEntity>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.Hide();
    }

    private void ShowTooltip()
    {
        if (owner != null)
        {
            switch (owner.IFF)
            {
                case IFF.BLUFOR:
                    tooltipText = "<color=blue>" + owner.Name + "\n" + owner.EntityList.Count.ToString() + 
                        " signatures</color>";
                    break;
                case IFF.OPFOR:
                    tooltipText = "<color=red>" + owner.Name + "\n" + owner.EntityList.Count.ToString() +
                        " signatures</color>";
                    break;
                default:
                    tooltipText = "<color=white>" + owner.Name + "</color>";
                    break;
            }

            Tooltip.Instance.Show(tooltipText);
        }
    }
}

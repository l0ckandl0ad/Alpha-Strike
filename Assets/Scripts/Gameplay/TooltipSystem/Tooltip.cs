using TMPro;
using UnityEngine;

public class Tooltip : Singleton<Tooltip>
{
    private RectTransform background;
    private TMP_Text text;
    private Vector2 paddingSize;
    
    [SerializeField]private Canvas canvas;
    private RectTransform canvasRectTransform;
    
    protected override void Awake()
    {
        base.Awake();
        
        background = transform.Find("background").GetComponent<RectTransform>();
        text = transform.Find("text").GetComponent<TMP_Text>();

        paddingSize = new Vector2(text.margin.x*2, text.margin.x*2);

        canvasRectTransform = canvas.GetComponent<RectTransform>();

        Hide();
    }

    private void SetText(string tooltipText)
    {
        text.SetText(tooltipText);
        text.ForceMeshUpdate();

        Vector2 textSize = text.GetRenderedValues(false);
        background.sizeDelta = textSize + paddingSize;
    }

    private void Update()
    {
        SetPosition(Input.mousePosition);
    }

    /// <summary>
    /// Set tooltip position while checking if it leaves the screen.
    /// </summary>
    private void SetPosition(Vector3 position)
    {
        if (position.x + background.rect.width > canvasRectTransform.rect.width)
        {
            // tootip is further than the screen size on the right
            position.x = canvasRectTransform.rect.width - background.rect.width;
        }
        if (position.y + background.rect.height > canvasRectTransform.rect.height)
        {
            // tooltip is higher than the screen size
            position.y = canvasRectTransform.rect.height - background.rect.height;
        }

        transform.position = position;
    }

    public void Show(string tooltipText)
    {
        SetPosition(Input.mousePosition);
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}

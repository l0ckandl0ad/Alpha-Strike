using UnityEngine;
using UnityEngine.UI;

public class MainPanelUI : MonoBehaviour
{
    [Header("Intel")]
    [SerializeField] private Button intelButton;
    [SerializeField] private IntelWindow intelWindow;

    private GameObject intelPanelGameObject;

    private void Start()
    {
        intelPanelGameObject = intelWindow.gameObject;
        intelPanelGameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        intelButton.onClick.AddListener(() => ToggleIntelPanel());
    }
    private void UnsubscribeFromEvents()
    {
        intelButton.onClick.RemoveListener(() => ToggleIntelPanel());
    }

    private void ToggleIntelPanel()
    {
        intelPanelGameObject.SetActive(!intelPanelGameObject.activeSelf);
    }
}

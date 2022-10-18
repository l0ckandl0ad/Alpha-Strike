using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the main panel that is shown when a friendly SpacePlatformGroup is selected.
/// This panel also enables/disables action buttons panel that's associated with it.
/// </summary>
public class ActivePlatformGroupPanel : MonoBehaviour
{
    private int prefabPoolSize = 100;
    private GameObject[] prefabPool;
    private SpacePlatformInfoView[] prefabComponentPool; // this is a twin of the prefabPool, used for its components!

    [Header("Internals")]
    [SerializeField] Transform scrollViewContent;
    [SerializeField] SpacePlatformInfoView spacePlatformInfoView;
    [SerializeField] GameObject actionsPanel;

    [Header("UI")]
    [SerializeField] private Text header;


    private void Awake()
    {
        InitializePrefabPool();
        Hide();
    }

    public void Show(ISpacePlatformGroup platformGroup)
    {
        if (platformGroup == null) return;

        InitializePanel();
        PopulatePanel(platformGroup);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        actionsPanel.SetActive(false);
    }

    private void InitializePanel()
    {
        if (prefabPool == null || prefabComponentPool == null)
        {
            InitializePrefabPool();
        }

        for (int i = 0; i < prefabPoolSize; i++)
        {
            prefabPool[i].SetActive(false);
        }

        gameObject.SetActive(true);
        actionsPanel.SetActive(true);
    }

    private void PopulatePanel(ISpacePlatformGroup platformGroup)
    {
        header.text = platformGroup.Name;

        UpdatePrefabs(platformGroup);
    }

    private void UpdatePrefabs(ISpacePlatformGroup platformGroup)
    {
        List<ISpacePlatform> platforms = platformGroup.GetSpacePlatforms();

        if (platforms == null)
        {
            Hide();
            return;
        }

        int count = platforms.Count;

        if (count == 0)
        {
            Hide();
            return;
        }

        for (int i = 0; i < count; i++)
        {
            prefabComponentPool[i].Initialize(platforms[i]);
        }
    }

    private void InitializePrefabPool()
    {
        prefabPool = new GameObject[prefabPoolSize];
        prefabComponentPool = new SpacePlatformInfoView[prefabPoolSize];

        for (int i = 0; i < prefabPoolSize; i++)
        {
            prefabPool[i] = Instantiate(spacePlatformInfoView.gameObject, scrollViewContent);
            prefabComponentPool[i] = prefabPool[i].GetComponent<SpacePlatformInfoView>();
            prefabPool[i].SetActive(false);
        }
    }
}
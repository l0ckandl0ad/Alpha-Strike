using System.Collections;
using UnityEngine;

public class FogOfWar : MonoBehaviour, IDetectable
{
    private bool isRunning = false;
    private float coroutinePulseDelay = 0.2f;
    public IMapEntity MapEntity { get; private set; }
    public IFF IFF { get => MapEntity.IFF; }
    public CircleCollider2D DetectableTriggerCollider2D { get; private set; }
    public bool IsDetectedByEnemy { get; private set; } = false;

    private void Awake()
    {
        CacheReferences();
        CreateDetectableTriggerCollider2D();
    }

    private void CacheReferences()
    {
        if (TryGetComponent(out IMapEntity mapEntity))
        {
            MapEntity = mapEntity;
        }
        else
        {
            Debug.Log("Error in FogOfWar.cs: " + this.ToString()
                + ". MapEntity not found on " + gameObject.ToString() + " gameObject.");
        }
    }
    private void CreateDetectableTriggerCollider2D()
    {
        if (DetectableTriggerCollider2D == null)
        {
            DetectableTriggerCollider2D = gameObject.AddComponent<CircleCollider2D>();
            DetectableTriggerCollider2D.isTrigger = true;
            DetectableTriggerCollider2D.radius = 0.001f;
        } 
    }

    private void Update()
    {
        if (!isRunning && !DateTimeModel.IsPaused)
        {
            StartCoroutine(FogOfWarCoroutine());
        }
    }

    private IEnumerator FogOfWarCoroutine()
    {
        isRunning = true;
        CacheReferences();
        HandleFogOfWar();
        yield return new WaitForSeconds(coroutinePulseDelay);
        isRunning = false;
    }

    private void HandleFogOfWar()
    {
        if (IsDetectedByEnemy) // when detected
        {
            IsDetectedByEnemy = false; // reset detected state until the next detection
            MapEntity.MakeVisible(true); // and make visible
        }
        else
        {
            if (IFF == GameSettings.EnemySide)
            {
                MapEntity.MakeVisible(false); // hide if an undetected enemy
            }
            else
            {
                MapEntity.MakeVisible(true); // show everyone else
            }
        }
    }

    public void Reveal()
    {
        IsDetectedByEnemy = true;
    }
}

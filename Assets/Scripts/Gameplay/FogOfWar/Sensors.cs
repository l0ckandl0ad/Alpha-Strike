using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    [SerializeField]private Material sensorMaterial;
    bool isSearching = false;
    private float sensorPulseRateInSeconds = 0.1f;
    private float basicScanRadius = 200f;
    private IFF scanerIFF;

    private List<IMapEntity> detectedEntityList = new List<IMapEntity>();
    public event Action<List<IMapEntity>> OnEntitiesDetected = delegate {};

    public IMapEntity MapEntity { get; private set; }
    

    void Start()
    {
        CacheReferences();
        UILibrary.DrawCircle(this.gameObject, basicScanRadius, 2f, sensorMaterial); // temp
    }

    private void CacheReferences()
    {
        if (TryGetComponent(out IMapEntity mapEntity))
        {
            MapEntity = mapEntity;
            scanerIFF = mapEntity.IFF;
        }
        else
        {
            Debug.Log("Error in Sensors.cs: " + this.ToString()
                + ". MapEntity not found on " + gameObject.ToString() + " gameObject.");
        }
    }

    private void BasicScan()
    {
        detectedEntityList.Clear();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, basicScanRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger)
            {
                if (collider.gameObject.TryGetComponent(out IDetectable detectableEntity))
                {
                    // refactor notes - this is bad because we don't check if specific entities are detectable,
                    // we're detecting the entire mapEntity. what if some entities are harder to detect?
                    // fog of war v2 needs to evaluate each entity inside the mapEntity
                    if (detectableEntity.IFF == GameSettings.GetOpponentSide(scanerIFF) &&
                        detectableEntity.MapEntity.EntityList.Count > 0)
                    {
                        detectableEntity.Reveal();
                        detectedEntityList.Add(detectableEntity.MapEntity);
                    }
                }
            }
        }

        if (detectedEntityList.Count > 0)
        {
            OnEntitiesDetected(detectedEntityList);
        }
    }

    private void Update()
    {
        if (!isSearching && !DateTimeModel.IsPaused)
        {
            StartCoroutine(SearchPulse());
        }
    }
    private IEnumerator SearchPulse()
    {
        isSearching = true;
        //CacheReferences(); // we would need this if change of IFF during gameplay is allowed
        BasicScan();
        yield return new WaitForSeconds(sensorPulseRateInSeconds);
        isSearching = false;
    }
}

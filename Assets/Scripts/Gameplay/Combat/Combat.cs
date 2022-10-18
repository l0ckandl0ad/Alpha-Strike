using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlphaStrike.Gameplay.Combat;
using System;

public class Combat : MonoBehaviour
{
    private bool isRunning = false; // is combat coroutine currently running?
    private readonly float combatRoundDelayInSeconds = 1f; // how much time combat coroutine waits before the next one


    private Sensors sensors; // we get sensor data about detected enemy mapEntities from here
    private IMapEntity mapEntity; // we'll be using our own mapEntity to get all ICombatants to run firing sequence
    private List<IEntity> entityList;

    // These can be switched at runtime to change target selection and firing behaviour
    // refactor note - can these patterns be made inside a static class?
    // their selection would be done differently then (via numerical/enum selector with switch or something like that)
    // would that be more efficient cpu/memory/gc wise?
    private ITargetingPattern currentTargetingPattern; // evaluates available targets and produces list for prosecution
    private IFiringPattern currentFiringPattern; // fires at targets according to specific pattern

    //public event Action<Transform, Transform> OnDirectFire = delegate { };

    private void Awake()
    {
        CacheReferences();

        SetTargetingPattern(new DefaultTargetingPattern()); // TEMP!
        SetFiringPattern(new DefaultFiringPattern()); // TEMP!
    }
    private void CacheReferences()
    {
        mapEntity = GetComponent<IMapEntity>();
        sensors = GetComponent<Sensors>();
        entityList = mapEntity.EntityList;

        if (mapEntity == null || sensors == null || entityList == null)
        {
            Debug.LogError(this + ": Cannot get critical components at Awake()!");
        }
    }
    private void OnEnable()
    {
        sensors.OnEntitiesDetected += CombatRound; // subscribe to getting sensor data to start combat with
    }
    private void OnDisable()
    {
        sensors.OnEntitiesDetected -= CombatRound; // unsub from getting sensor data
    }
    public void SetTargetingPattern(ITargetingPattern targetingPattern)
    {
        currentTargetingPattern = targetingPattern;
    }
    public void SetFiringPattern(IFiringPattern firingPattern)
    {
        currentFiringPattern = firingPattern;
    }

    private void CombatRound(List<IMapEntity> detectedEnemies) // WIP / DEBUG!
    {
        if (!isRunning && !DateTimeModel.IsPaused)
        {
            // Generate a list of target data here
            List<TargetData> availableTargets = CombatUtils.GetTargetDataList(mapEntity, detectedEnemies);
            // And pass it into coroutine
            StartCoroutine(combatCoroutine(availableTargets));
        }
    }

    private IEnumerator combatCoroutine(List<TargetData> availableTargets)
    {
        isRunning = true;
        if (availableTargets?.Count > 0)
        {
            currentFiringPattern.Fire(CombatUtils.EntityToCombatantList(entityList),
                currentTargetingPattern.SortTargets(availableTargets));
        }
        yield return new WaitForSeconds(combatRoundDelayInSeconds);
        isRunning = false;
    }

    private void OnDestroy()
    {
        // do some cleanup work for coroutine's premature ending?
    }

}

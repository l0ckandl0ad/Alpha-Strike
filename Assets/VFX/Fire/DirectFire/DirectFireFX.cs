using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectFireFX : MonoBehaviour
{
    [SerializeField] private GameObject beamPrefab;

    [SerializeField] private float beamLifetimeInSeconds = 1f;

    private void Awake()
    {
        // TODO: subscribe Fire() to mapentity to listen for firing events to run
    }

    private void Fire()
    {
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        // TODO spawn prefab

        // TODO initialize prefab with targeting data

        // TODO show prefab

        yield return new WaitForSeconds(beamLifetimeInSeconds);

        // TODO: destroy spawned prefab
    }

    private void OnDestroy()
    {
        // TODO: unsubscribe Fire() from listening for firing events on mapentity
    }
}

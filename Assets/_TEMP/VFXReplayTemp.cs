using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXReplayTemp : MonoBehaviour
{
    bool isRunning = false;
    private VisualEffect vfx;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            StartCoroutine(ShowEffect());
        }
        
    }

    IEnumerator ShowEffect()
    {
        isRunning = true;

        yield return new WaitForSeconds(1f);

        vfx.Play();

        isRunning = false;

    }
}

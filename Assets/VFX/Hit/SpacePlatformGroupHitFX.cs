using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpacePlatformGroupHitFX : MonoBehaviour
{
    private ISpacePlatformGroup spacePlatformGroup;
    private VisualEffect vfx;

    private void Start()
    {
        vfx = GetComponent<VisualEffect>();
        spacePlatformGroup = GetComponentInParent<ISpacePlatformGroup>();

        if (spacePlatformGroup != null && vfx != null)
        {
            spacePlatformGroup.OnHit += PlayVFX;
        }
        else
        {
            Debug.LogError(this + ": critical components not found!");
        }
    }
    
    private void PlayVFX(ISpacePlatform spacePlatform)
    {
        vfx.Play();
    }

    private void OnDestroy()
    {
        if (spacePlatformGroup != null) spacePlatformGroup.OnHit -= PlayVFX;
    }
}

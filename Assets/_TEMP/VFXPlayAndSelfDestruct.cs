using UnityEngine;
using UnityEngine.VFX;

public class VFXPlayAndSelfDestruct : MonoBehaviour
{
    [SerializeField] private float durationInSeconds = 0.5f;
    private VisualEffect vfx;
    // Start is called before the first frame update
    void Awake()
    {
        vfx = GetComponent<VisualEffect>();
        
        Destroy(this.gameObject, durationInSeconds);
    }

    private void Start()
    {
        vfx.Play();
    }
}

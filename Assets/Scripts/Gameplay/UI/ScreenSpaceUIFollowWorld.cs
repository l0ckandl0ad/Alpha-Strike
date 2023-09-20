using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceUIFollowWorld : MonoBehaviour
{
    private Camera cam;

    private Vector3 position;


    private void Start()
    {
        cam = Camera.main;
        position = transform.position;
    }

    private void Update()
    {
        position = cam.WorldToScreenPoint(transform.position);
    }
}

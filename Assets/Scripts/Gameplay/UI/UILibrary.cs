using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a static class to store any UI related functions that are called often. It is kind of shitty at the moment.
/// </summary>
public static class UILibrary
{
    public static void DrawCircle(GameObject container, float radius, float lineWidth, Material material)
    {
        int segments = 360;
        var line = container.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;
        line.material = material;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        line.SetPositions(points);
    }
}

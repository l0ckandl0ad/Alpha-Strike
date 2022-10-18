using UnityEngine;

namespace AlphaStrike.Gameplay.Utils
{
    public static class VectorUtils
    {
        /// <summary>
        /// Get Vector2 offset from another Vector2 using distance and angle in degrees.
        ///  Angle 0 = North. Min/max angle = 0-360.
        /// </summary>
        public static Vector2 VectorWithOffset(Vector2 position, float distance, float angleInDegrees)
        {
            Vector2 newPosition = position;

            float x = distance * Mathf.Sin(angleInDegrees * Mathf.Deg2Rad);
            float y = distance * Mathf.Cos(angleInDegrees * Mathf.Deg2Rad);
            newPosition.x += x;
            newPosition.y += y;

            return newPosition;
        }
    }
}
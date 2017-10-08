using UnityEngine;

namespace BrainVR.UnityFramework.Helpers
{
    public static class InstantiatingHelper
    {
        public static Vector3 GetWorldPositionFromVector2(Vector2 XY, Vector3 center)
        {
            return new Vector3(center.x + XY.x, center.y, center.z + XY.y);
        }
    }
}

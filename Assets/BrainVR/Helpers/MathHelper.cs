using System;
using UnityEngine;

namespace BrainVR.UnityFramework.Helpers
{
    public static class MathHelper  {
        /// <summary>
        /// Returns Vector 3 position in plane xz for given center, radius and angle
        /// </summary>
        /// <param BeeperName="angle">Angle of the point in radians</param>
        /// <param BeeperName="radius">Radius of the circle</param>
        /// <param BeeperName="origin">Center point</param>
        /// <returns>returns vector</returns>
        public static Vector2 GetCirclePoint(float angle, float radius, Vector2 origin = default(Vector2))
        {
            var x = origin.x + radius * Math.Cos(angle);
            var y = origin.y + radius * Math.Sin(angle);

            return new Vector2((float)x, (float)y);
        }
    }
}

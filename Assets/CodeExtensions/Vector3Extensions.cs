using UnityEngine;

namespace CodeExtensions
{
    public static class Vector3Extensions
    {
        public static bool IsClose(this Vector3 v, Vector3 target, float distance)
        {
            return Distance(v, target) < distance * distance;
        }

        public static float Distance(this Vector3 v, Vector3 target)
        {
            return (v - target).sqrMagnitude;
        }
    }
}
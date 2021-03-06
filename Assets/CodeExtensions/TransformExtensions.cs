﻿using System.Linq;
using UnityEngine;

namespace CodeExtensions
{
    public static class TransformExtensions
    {
        public static bool IsInDistance(this Transform transform, Vector3 x, float maxDistance)
        {
            return (transform.position - x).sqrMagnitude < maxDistance*maxDistance;
        }

        public static void Rotate2DToPosition(this Transform transform, Vector3 vector)
        {
            vector = (vector - transform.position).normalized;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
        }

        public static bool IsEntityChild(this Transform transform, Transform potentialChild, out Transform entityParent)
        {
            entityParent = transform;
            while (true)
            {
                if (entityParent.CompareTag("EntityRoot")) { break; }

                entityParent = entityParent.parent;
                if (entityParent != null) { continue; }

                return false;
            }

            return entityParent.GetComponentsInChildren<Transform>().Any(x => x.Equals(potentialChild));
        }
    }
}

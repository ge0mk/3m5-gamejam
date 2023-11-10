using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gianni.Helper
{
    public static class Vector3Extansion
    {
        public static Vector2 ToVector2XZ(this Vector3 from)
        {
            return new Vector2(from.x, from.z);
        }
        public static Vector3 ToVectorXZ(this Vector2 from)
        {
            return new Vector3(from.x, 0f, from.y);
        }
        public static Vector3 ToVectorXZ(this Vector3 from)
        {
            return new Vector3(from.x, 0f, from.y);
        }

        public static bool IsGreaterOrEqual(this Vector3 local, Vector3 other)
        {
            if (local.x >= other.x && local.y >= other.y && local.z >= other.z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsLesserOrEqual(this Vector3 local, Vector3 other)
        {
            if (local.x <= other.x && local.y <= other.y && local.z <= other.z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector3 RadianToVector2(float radian)
        {
            return new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian));
        }

        public static Vector3 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
        public static float ToRadian(this Vector3 relative)
        {
            return Mathf.Atan2(relative.x, relative.z);
        }
        public static float ToDegree(this Vector3 relative)
        {
            return Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        }
    } 
}

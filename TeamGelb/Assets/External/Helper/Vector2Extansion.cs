using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GellosGames.Assets._scripts.Helper
{
    public static class Vector2Extansion
    {
        public static bool IsQuiteZero(this Vector2 local, float tolerance)
        {
            return local.magnitude < tolerance;
        }
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
        public static float ToRadian(this Vector2 relative)
        {
            return Mathf.Atan2(relative.x, relative.y);
        }
        public static float ToDegree(this Vector2 relative)
        {
            return Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        }
    }
}

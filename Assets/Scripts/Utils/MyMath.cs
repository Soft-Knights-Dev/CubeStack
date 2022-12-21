using UnityEngine;

namespace Utils
{
    public static class MyMath
    {
        public static float ClampF(float v) =>
            Mathf.Clamp(v, 0, float.MaxValue);

        public static Vector3 AbsVector(this Vector3 v) =>
            new (Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));

        public static Vector3 MultiplyVector(this Vector3 v, Vector3 o) =>
            new(v.x * o.x, v.y * o.y, v.z * o.z);

    }
}
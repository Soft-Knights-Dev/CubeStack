using UnityEngine;

namespace Utils
{
    public static class LoggerUtils
    {
        public static void LogVector(Vector2 v) => Debug.Log($"Vector2 -> x: {v.x} y: {v.y}");
        public static void LogVector(Vector3 v) => Debug.Log($"Vector3 -> x: {v.x} y: {v.y} z: {v.z}");
    }
}
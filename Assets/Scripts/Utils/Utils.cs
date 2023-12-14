using UnityEngine;

namespace Utils
{
    internal static class SupportFunctions
    {
        public static Vector3 DefinePointInArea(Transform spawnPointArea, int spawnRadius, float positionY)
        {
           // float positionY = 0.2f;
            Vector3 point = (Random.insideUnitSphere * spawnRadius) + spawnPointArea.position;

            return new Vector3(point.x, positionY, point.z);
        }
    }
}
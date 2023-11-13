using UnityEngine;

namespace Utils
{
    internal static class SupportSpawnerFunction
    {
        public static Vector3 DefinePointInArea(Transform spawnPointArea, int spawnRadius)
        {
            Vector3 point = (Random.insideUnitSphere * spawnRadius) + spawnPointArea.position;

            return new Vector3(point.x, 0.2f, point.z);
        }
    }
}
using UnityEngine;

namespace Utils
{
    internal static class SupportFunction
    {
        public static float Epsilon = 0.01f;
        
        public static Vector3 DefinePointInArea(Transform spawnPointArea, int spawnRadius)
        {
            float positionY = 0.2f;
            Vector3 point = (Random.insideUnitSphere * spawnRadius) + spawnPointArea.position;

            return new Vector3(point.x, positionY, point.z);
        }
        
    }
    
    public enum WorkerState
    {
        Idle,
        MoveToBarrel,
        MoveToStock,
        MoveToWaitingPoint
    }
    
    public enum BarrelState
    {
        Idle,
        InAction
    }
}
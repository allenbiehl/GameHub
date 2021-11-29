using UnityEngine;

namespace GameHub.Core.Util
{
    public class PrefabLoader
    {
        public static GameObject Load(PrefabType prefab)
        {
            return UnityEngine.Resources.Load($"Prefabs/{prefab.ToString()}", typeof(GameObject)) as GameObject;
        }
    }
}
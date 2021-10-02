using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu (menuName = "Configs/MapConfig", fileName = "MapConfig", order = 2)]
    public class LevelGeneratorConfig:ScriptableObject
    {
        public GameObject[] Prefabs;
        public float MinLenght;
        public float MaxLenght;
    }
}
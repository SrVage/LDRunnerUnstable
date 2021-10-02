using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu (menuName = "Configs/PotionConfig", fileName = "PotionConfig", order = 6)]
    public class PotionConfig:ScriptableObject
    {
        public GameObject PotionPrefab;
        public float MinTime;
        public float MaxTime;
    }
}
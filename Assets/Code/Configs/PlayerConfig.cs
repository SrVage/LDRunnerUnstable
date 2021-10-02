using TMPro;
using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu (menuName = "Configs/PlayerConfig", fileName = "PlayerConfig", order = 1)]
    public class PlayerConfig:ScriptableObject
    {
        public GameObject PlayerPrefab;
        public float PlayerSpeed;
    }
}
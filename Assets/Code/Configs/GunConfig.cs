using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu (menuName = "Configs/GunConfig", fileName = "GunConfig", order = 3)]
    public class GunConfig:ScriptableObject
    {
        public GameObject GunPrefab;
        public float RechargeTime;
    }
}
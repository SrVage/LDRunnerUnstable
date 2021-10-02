using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu (menuName = "Configs/BulletConfig", fileName = "BulletConfig", order = 4)]
    public class BulletConfig:ScriptableObject
    {
        public GameObject BulletPrafab;
        public float BulletForce;
    }
}
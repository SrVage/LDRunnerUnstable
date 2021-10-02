using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu (menuName = "Configs/UIConfig", fileName = "UIConfig", order = 5)]
    public class UIConfig:ScriptableObject
    {
        public GameObject RadiationIndicator;
    }
}
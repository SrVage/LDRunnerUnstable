using Client.Components;
using Client.Configs;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Systems
{
    public class RadiationIndicatorSystem:IEcsInitSystem, IEcsRunSystem
    {
        private readonly UIConfig _uIConfig;
        private readonly EcsWorld _world;
        private readonly EcsFilter<Radiation> _radiation;
        private readonly EcsFilter<RadiationIndicator> _radiationIndicator;
        public void Init()
        {
            _world.NewEntity().Get<Radiation>();
            var canvas = GameObject.Instantiate(_uIConfig.RadiationIndicator);
            var radiationIndicator = canvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
            var radiationIndicatorEntity = _world.NewEntity();
            ref var indicator = ref radiationIndicatorEntity.Get<RadiationIndicator>().Indicator;
            indicator = radiationIndicator;
        }

        public void Run()
        {
            foreach (var rad in _radiation)
            {
                ref var variable = ref _radiation.Get1(rad).RadiationValue;
                foreach (var radInd in _radiationIndicator)
                {
                    ref var indicator = ref _radiationIndicator.Get1(radInd).Indicator;
                    indicator.fillAmount = variable;
                }
            }
        }
    }
}
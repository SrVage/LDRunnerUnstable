using Client.Components;
using Client.Configs;
using Client.MonoBehs;
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
        private readonly EcsFilter<PlayState> _playState;
        public void Init()
        {
            _world.NewEntity().Get<Radiation>();
            var canvas = GameObject.Instantiate(_uIConfig.RadiationIndicator).GetComponent<CanvasInit>();
            var entity = _world.NewEntity();
            entity.Get<PlayState>().PlayStates = PlayStates.Menu;
            canvas.Init(entity, _world);
            var radiationIndicator = canvas.Radiation;
            var radiationIndicatorEntity = _world.NewEntity();
            ref var indicator = ref radiationIndicatorEntity.Get<RadiationIndicator>().Indicator;
            indicator = radiationIndicator;
        }

        public void Run()
        {
            foreach (var play in _playState)
            {
                ref var playState = ref _playState.Get1(play).PlayStates;
                if (playState != PlayStates.Play)
                    return;
            }
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
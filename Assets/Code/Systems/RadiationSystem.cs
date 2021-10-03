using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class RadiationSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Radiation> _radiation;
        private readonly EcsFilter<GetPotion> _potion;
        private readonly EcsFilter<PlayState> _playState;
        public void Run()
        {
            foreach (var play in _playState)
            {
                ref var playState = ref _playState.Get1(play).PlayStates;
                if (playState == PlayStates.Play)
                {
                    foreach (var potion in _potion)
                    {
                        foreach (var rad in _radiation)
                        {
                            ref var radiation = ref _radiation.Get1(rad).RadiationValue;
                            if (radiation > 0.5f)
                                radiation -= 0.5f;
                            else
                                radiation = 0;
                        }
                        _potion.GetEntity(potion).Destroy();
                    }
                    if (_radiation.IsEmpty()) return;
                    foreach (var rad in _radiation)
                    {
                        ref var radiation = ref _radiation.Get1(rad).RadiationValue;
                        radiation += (Time.deltaTime / 20);
                    }
                }

                if (playState == PlayStates.Menu)
                {
                    if (_radiation.IsEmpty()) return;
                    foreach (var rad in _radiation)
                    {
                        ref var radiation = ref _radiation.Get1(rad).RadiationValue;
                        radiation = 0;
                    }
                }
            }

        }
    }
}
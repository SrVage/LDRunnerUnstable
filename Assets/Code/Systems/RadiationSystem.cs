using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class RadiationSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Radiation> _radiation;
        private readonly EcsFilter<GetPotion> _potion;
        public void Run()
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
    }
}
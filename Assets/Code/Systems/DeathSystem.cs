using System;
using Client.Components;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class DeathSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Radiation> _radiation;
        private readonly EcsFilter<Player, Physic, Animation> _player;
        public void Run()
        {
            foreach (var rad in _radiation)
            {
                ref var radiation = ref _radiation.Get1(rad).RadiationValue;
                if (radiation >= 1)
                {
                    foreach (var player in _player)
                    {
                        _player.GetEntity(player).Del<Physic>();
                        _player.Get3(player).Animator.SetTrigger("Death");
                        _player.GetEntity(player).Get<Death>().Time = 4f;
                    }
                }
            }
        }
    }
}
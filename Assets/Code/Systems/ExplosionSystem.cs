using System;
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class ExplosionSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Explosion, Death, ObjectTransform> _player;
        private readonly EcsFilter<PlayState> _playState;

        public void Run()
        {
            foreach (var player in _player)
            {
                ref var time = ref _player.Get2(player).Time;
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    return;
                }
                _player.Get1(player).Particle.Play();
                ref var gameObj = ref _player.Get3(player).GameObject;
                GameObject.Destroy(gameObj, 0.2f);
                _player.GetEntity(player).Destroy();
                foreach (var play in _playState)
                {
                    ref var playState = ref _playState.Get1(play).PlayStates;
                    playState = PlayStates.Menu;
                }

            }
        }
    }
}
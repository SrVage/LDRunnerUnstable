using System;
using Client.Components;
using Client.Configs;
using Client.MonoBehs;
using Leopotam.Ecs;
using UnityEngine;
using Animation = Client.Components.Animation;
using Renderer = Client.Components.Renderer;

namespace Client.Systems
{
    public class PlayerInitializeSystem:IEcsRunSystem
    {
        private readonly PlayerConfig _playerConfig;
        private readonly EcsFilter<PlayState> _playState;
        private readonly EcsWorld _world;

        public void Run()
        {
            foreach (var playstate in _playState)
            {
                ref var playState = ref _playState.Get1(playstate).PlayStates;
                if (playState != PlayStates.EnterLevel) return;
                var playerEntity = _world.NewEntity();
                var player = GameObject.Instantiate(_playerConfig.PlayerPrefab, new Vector3(2,2,0), Quaternion.identity);
                playerEntity.Get<Player>().GunPosition = player.GetComponent<PlayerViewRefs>().GunPosition;
                playerEntity.Get<ObjectTransform>().GameObject = player;
                playerEntity.Get<ObjectTransform>().Transform = player.transform;
                playerEntity.Get<Physic>().Rigidbody = player.GetComponent<Rigidbody>();
                playerEntity.Get<Animation>().Animator = player.GetComponent<PlayerViewRefs>().Animator;
                playerEntity.Get<Renderer>().MeshRenderer = player.GetComponent<PlayerViewRefs>().MeshRenderer;
                playerEntity.Get<Explosion>().Particle = player.GetComponent<PlayerViewRefs>().ParticleSystem;
                playState = PlayStates.Play;
            }
        }
    }
}
using Client.Components;
using Client.Configs;
using Client.MonoBehs;
using Leopotam.Ecs;
using UnityEngine;
using Animation = Client.Components.Animation;
using Renderer = Client.Components.Renderer;

namespace Client.Systems
{
    public class PlayerInitializeSystem:IEcsInitSystem
    {
        private readonly PlayerConfig _playerConfig;
        private readonly EcsWorld _world;
        public void Init()
        {
            var playerEntity = _world.NewEntity();
            var player = GameObject.Instantiate(_playerConfig.PlayerPrefab, Vector3.zero, Quaternion.identity);
            playerEntity.Get<Player>().GunPosition = player.GetComponent<PlayerViewRefs>().GunPosition;
            playerEntity.Get<ObjectTransform>().GameObject = player;
            playerEntity.Get<ObjectTransform>().Transform = player.transform;
            playerEntity.Get<Physic>().Rigidbody = player.GetComponent<Rigidbody>();
            playerEntity.Get<Animation>().Animator = player.GetComponent<PlayerViewRefs>().Animator;
            playerEntity.Get<Renderer>().MeshRenderer = player.GetComponent<PlayerViewRefs>().MeshRenderer;
        }
    }
}
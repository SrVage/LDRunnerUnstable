using Cinemachine;
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class CameraFollowSystem:IEcsRunSystem
    {
        private readonly EcsFilter<ObjectTransform, Player> _player;
        private readonly EcsFilter<CameraEntity> _camera;

        public CameraFollowSystem(CinemachineVirtualCamera camera, EcsWorld _world)
        {
            _world.NewEntity().Get<CameraEntity>().Transform = camera;
        }
        public void Run()
        {
            foreach (var camera in _camera)
            {
                foreach (var player in _player)
                {
                    ref var play = ref _player.Get1(player).Transform;
                    ref var camTransform = ref _camera.Get1(camera).Transform;
                    camTransform.m_Follow = play;
                }
            }
        }
    }
}
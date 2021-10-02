using System;
using Client.Components;
using Client.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class GunAimingSystem:IEcsInitSystem,IEcsRunSystem
    {
        private readonly GunConfig _gunConfig;
        private readonly EcsFilter<Aim> _aim;
        private readonly EcsFilter<ObjectTransform, Player> _player;
        private readonly EcsWorld _world;
        private readonly EcsFilter<ObjectTransform, Gun> _gun;
        public void Init()
        {
            var gun = GameObject.Instantiate(_gunConfig.GunPrefab, Vector3.zero, Quaternion.identity);
            var gunEntity = _world.NewEntity();
            gunEntity.Get<Gun>();
            gunEntity.Get<ObjectTransform>().Transform = gun.transform;
            gunEntity.Get<ObjectTransform>().GameObject = gun;
        }
        public void Run()
        {
            foreach (var player in _player)
            { 
                ref var transform = ref _player.Get2(player).GunPosition;
                foreach (var gun in _gun)
                {
                    ref var time = ref _gun.Get2(gun).Time;
                    time -= Time.deltaTime;
                    ref var gunTransform = ref _gun.Get1(gun).Transform;
                    gunTransform.position = transform.position;
                    foreach (var aim in _aim)
                    {
                        ref var angle = ref _aim.Get1(aim).Angle; 
                        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0 , angle));
                    }
                }
            }
        }
    }
}
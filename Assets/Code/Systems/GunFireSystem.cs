using System;
using Client.Components;
using Client.Configs;
using Client.Services;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class GunFireSystem:IEcsInitSystem,IEcsRunSystem
    {
        private readonly EcsFilter<ObjectTransform, Gun> _gun;
        private readonly EcsFilter<Aim, InputTouch> _aim;
        private readonly EcsFilter<BulletPoolRef> _bulletPool;
        private readonly BulletConfig _bulletConfig;
        private readonly GunConfig _gunConfig;
        private readonly EcsWorld _world;
        public void Init()
        {
            var pool = new BulletPool(_bulletConfig.BulletPrafab);
            var poolEntity = _world.NewEntity();
            ref var poolEnt = ref poolEntity.Get<BulletPoolRef>().BulletPool;
            poolEnt = pool;
        }

        public void Run()
        {
            foreach (var aim in _aim)
            {
                ref var angle = ref _aim.Get1(aim).Angle;
                {
                    foreach (var gun in _gun)
                    {
                        ref var firePosition = ref _gun.Get1(gun).Transform;
                        ref var time = ref _gun.Get2(gun).Time;
                        if (time>0) break;
                        time = _gunConfig.RechargeTime;
                        foreach (var pool in _bulletPool)
                        {
                            var bullet = _bulletPool.Get1(pool).BulletPool.GetBullet();
                            bullet.transform.position = firePosition.position;
                            Vector3 direction = new Vector3(1*Mathf.Cos(angle*Mathf.Deg2Rad), 1*Mathf.Sin(angle*Mathf.Deg2Rad), 0).normalized;
                            bullet.GetComponent<Rigidbody>().AddForce(direction*_bulletConfig.BulletForce, ForceMode.Impulse);
                        }
                    }
                }
                _aim.GetEntity(aim).Destroy();
            }
        }
    }
}
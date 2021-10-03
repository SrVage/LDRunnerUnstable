using System;
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class DestroyBulletSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Bullet, ObjectTransform> _bullet;
        private readonly EcsFilter<BulletPoolRef> _bulletPool;
        public void Run()
        {
            foreach (var bul in _bullet)
            {
                ref var bulletTime = ref _bullet.Get1(bul).Time;
                if (bulletTime > 0)
                {
                    bulletTime -= Time.deltaTime;
                    return;
                }

                ref var bullet = ref _bullet.Get2(bul).GameObject;
                ref var bulletEntity = ref _bullet.GetEntity(bul);
                foreach (var pool in _bulletPool)
                {
                    ref var bulletPool = ref _bulletPool.Get1(pool).BulletPool;
                    bulletPool.ReturnToPool(bullet);
                    bulletEntity.Destroy();
                    break;
                }
            }
        }
    }
}
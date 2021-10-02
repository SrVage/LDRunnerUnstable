using System;
using Client.Components;
using Client.Configs;
using Client.MonoBehs;
using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Client.Systems
{
    public class PotionCreateSystem:IEcsRunSystem
    {
        private readonly PotionConfig _potionConfig;
        private readonly EcsWorld _world;
        private readonly EcsFilter<Block> _block;
        private readonly EcsFilter<BlockCount> _count;
        private float _time = 0;

        public void Run()
        {
            if (_time > 0)
            {
                _time -= Time.deltaTime;
                return;
            }
            foreach (var c in _count)
            {
                ref var count = ref _count.Get1(c).Count;
                foreach (var block in _block)
                {
                    ref var blockCount = ref _block.Get1(block).Count;
                    if (blockCount == count)
                    {
                        ref var start = ref _block.Get1(block).StartPoint;
                        ref var end = ref _block.Get1(block).EndPoint;
                        var position = Vector3.Lerp(start, end, (float)Random.Range(10, 90)/100)+Vector3.up*Random.Range(5f, 8f);
                        GameObject.Instantiate(_potionConfig.PotionPrefab, position, Quaternion.identity).GetComponent<PotionTrigger>().Init(_world);
                        _time = Random.Range(_potionConfig.MinTime, _potionConfig.MaxTime);
                        break;
                    }
                }
            }
        }
    }
}
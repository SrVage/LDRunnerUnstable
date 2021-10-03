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
        private readonly EcsFilter<PlayState> _playState;
        private readonly EcsFilter<Potion>.Exclude<Destroy> _potion;
        private float _time = 0;

        public void Run()
        {
            foreach (var play in _playState)
            {
                ref var playState = ref _playState.Get1(play).PlayStates;
                if (playState == PlayStates.Play)
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
                                var position = Vector3.Lerp(start, end, (float)Random.Range(10, 90)/100)+Vector3.up*Random.Range(5f, 7f);
                                var potionGO = GameObject.Instantiate(_potionConfig.PotionPrefab, position, Quaternion.identity);
                                potionGO.GetComponent<PotionTrigger>().Init(_world);
                                var potionEntity = _world.NewEntity();
                                potionEntity.Get<Potion>();
                                potionEntity.Get<ObjectTransform>().GameObject = potionGO;
                                potionEntity.Get<ObjectTransform>().Transform = potionGO.transform;
                                _time = Random.Range(_potionConfig.MinTime, _potionConfig.MaxTime);
                                break;
                            }
                        }
                    }
                }

                if (playState == PlayStates.Menu)
                {
                    foreach (var pot in _potion)
                    {
                        _potion.GetEntity(pot).Get<Destroy>();
                    }
                }
            }

        }
    }
}
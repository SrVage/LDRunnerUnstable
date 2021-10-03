using Client.Components;
using Client.Configs;
using Client.MonoBehs;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class LevelGeneratorSystem:IEcsInitSystem,IEcsRunSystem
    {
        private readonly LevelGeneratorConfig _levelGeneratorConfig;
        private readonly EcsFilter<BlockCount> _count;
        private readonly EcsFilter<Block, ObjectTransform> _block;
        private readonly EcsFilter<PlayState> _playState;
        private readonly EcsWorld _world;


        public void Init()
        {
            ref var count = ref  _world.NewEntity().Get<BlockCount>().Count;
            Vector3 offset = new Vector3(Random.Range(0, 3), Random.Range(-3, 3), 0);
            var newBlock =
                GameObject.Instantiate(
                    _levelGeneratorConfig.Prefabs[Random.Range(0, _levelGeneratorConfig.Prefabs.Length - 1)],
                    Vector3.down, Quaternion.identity);
            newBlock.transform.localScale =
                new Vector3(1 * Random.Range(_levelGeneratorConfig.MinLenght, _levelGeneratorConfig.MaxLenght), 1, 1);
            CreateBlockEntity(newBlock, ref count);
        }

        private void CreateBlockEntity(GameObject newBlock, ref int count)
        {
            var newBlockEntity = _world.NewEntity();
            count++;
            newBlockEntity.Get<ObjectTransform>().GameObject = newBlock;
            newBlockEntity.Get<ObjectTransform>().Transform = newBlock.transform;
            newBlockEntity.Get<Block>().Count = count;
            newBlockEntity.Get<Block>().StartPoint = newBlock.GetComponent<BlockViewRefs>().StartPoint.position;
            newBlockEntity.Get<Block>().EndPoint = newBlock.GetComponent<BlockViewRefs>().EndPoint.position;
        }

        public void Run()
        {
            foreach (var play in _playState)
            {
                
                ref var playState = ref _playState.Get1(play).PlayStates;
                if (playState == PlayStates.Menu)
                {
                    foreach (var block in _block)
                    {
                        _block.GetEntity(block).Get<Destroy>();
                    }
                    foreach (var c in _count)
                    {
                        ref var count = ref _count.Get1(c).Count;
                        count = 0;
                    }
                }

                if (playState == PlayStates.Play)
                {
                    if (_block.GetEntitiesCount() > 3) return;
                                Vector3 endPoint = Vector3.zero;
                                foreach (var c in _count)
                                {
                                    ref var count = ref _count.Get1(c).Count;
                                    foreach (var block in _block)
                                    {
                                        ref var blockEntity = ref _block.Get1(block);
                                        if (blockEntity.Count == count)
                                        {
                                            endPoint = blockEntity.EndPoint;
                                            break;
                                        }
                                    }
                    
                    
                                    Vector3 offset = new Vector3(Random.Range(2, 4), Random.Range(-2, 2), 0);
                                    Vector3 position = new Vector3(endPoint.x + offset.x,
                                        Mathf.Clamp((endPoint.y + offset.y), -15, 15), 0);
                                    var newBlock =
                                        GameObject.Instantiate(
                                            _levelGeneratorConfig.Prefabs[Random.Range(0, _levelGeneratorConfig.Prefabs.Length)],
                                            position, Quaternion.identity);
                                    newBlock.transform.localScale =
                                        new Vector3(1 * Random.Range(_levelGeneratorConfig.MinLenght, _levelGeneratorConfig.MaxLenght), 1,
                                            1);
                                    CreateBlockEntity(newBlock, ref count);
                                }
                }
            }
            
        }


    }
}
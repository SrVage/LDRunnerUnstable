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
            if (_block.GetEntitiesCount() > 6) return;
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


                Vector3 offset = new Vector3(Random.Range(1, 3), Random.Range(-2, 2), 0);
                var newBlock =
                    GameObject.Instantiate(
                        _levelGeneratorConfig.Prefabs[Random.Range(0, _levelGeneratorConfig.Prefabs.Length - 1)],
                        (endPoint + offset), Quaternion.identity);
                newBlock.transform.localScale =
                    new Vector3(1 * Random.Range(_levelGeneratorConfig.MinLenght, _levelGeneratorConfig.MaxLenght), 1,
                        1);
                CreateBlockEntity(newBlock, ref count);
            }
        }


    }
}
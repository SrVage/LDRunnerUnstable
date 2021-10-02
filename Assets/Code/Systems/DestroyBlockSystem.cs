using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class DestroyBlockSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Block, ObjectTransform> _block;
        private readonly EcsFilter<ObjectTransform, Player> _player;
        public void Run()
        {
            foreach (var player in _player)
            {
                ref var play = ref _player.Get1(player).Transform;
                foreach (var block in _block)
                {
                    ref var blockTransform = ref _block.Get2(block).Transform;
                    if (blockTransform.position.x< play.position.x-15)
                    {
                        GameObject.Destroy(blockTransform.gameObject);
                        _block.GetEntity(block).Destroy();
                    }
                }
            }
        }
    }
}
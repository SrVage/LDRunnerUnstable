using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class DestroySystem:IEcsRunSystem
    {
        private readonly EcsFilter<ObjectTransform, Destroy> _destroy;
        public void Run()
        {
            foreach (var i in _destroy)
            {
                ref var destroy = ref _destroy.Get1(i).GameObject;
                GameObject.Destroy(destroy);
                _destroy.GetEntity(i).Destroy();
            }
        }
    }
}
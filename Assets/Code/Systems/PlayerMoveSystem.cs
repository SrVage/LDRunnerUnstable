using Client.Components;
using Client.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class PlayerMoveSystem:IEcsRunSystem
    {
        private readonly EcsFilter<ObjectTransform, Player, Physic>.Exclude<Death> _player;
        private readonly EcsFilter<InputTouch, TouchTime> _touch;
        private readonly PlayerConfig _playerConfig;
        
        public void Run()
        {
            foreach (var player in _player)
            {
                ref var physic = ref _player.Get3(player);
                if (Mathf.Abs(physic.Rigidbody.velocity.y)<0.01f)
                    physic.Rigidbody.velocity = new Vector3(_playerConfig.PlayerSpeed, physic.Rigidbody.velocity.y, 0);
                if (physic.Rigidbody.velocity.y < -40)
                    _player.GetEntity(player).Get<Death>().Time = 1;
            }
            foreach (var touch in _touch)
            {
                ref var time = ref _touch.Get2(touch).Time;
                foreach (var player in _player)
                {
                     ref var physic = ref _player.Get3(player);
                     if (Mathf.Abs(physic.Rigidbody.velocity.y)<0.01f)
                         physic.Rigidbody.AddForce(3000*Vector3.up*time);
                }
                _touch.GetEntity(touch).Destroy();
            }
        }
    }
}
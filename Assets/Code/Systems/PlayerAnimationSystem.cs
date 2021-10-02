using Client.Components;
using Leopotam.Ecs;
using UnityEngine;
using Animation = Client.Components.Animation;

namespace Client.Systems
{
    public class PlayerAnimationSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Player, Physic, Animation> _player;
        private bool _ready = true;
        public void Run()
        {
            foreach (var player in _player)
            {
                ref var physics = ref _player.Get2(player).Rigidbody;
                if (Mathf.Abs(physics.velocity.y) > 0.1f && _ready)
                {
                    ref var animator = ref _player.Get3(player).Animator;
                    animator.SetTrigger("Jump");
                    _ready = false;
                }
                if (physics.velocity.y == 0 && !_ready)
                {
                    _ready = true;
                    ref var animator = ref _player.Get3(player).Animator;
                    animator.SetTrigger("Fall");
                }
            }
        }
    }
}
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;
using Animation = Client.Components.Animation;

namespace Client.Systems
{
    public class PlayerAnimationSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Player, Physic, Animation> _player;
        private readonly EcsFilter<PlayState> _playState;
        private bool _ready = true;
        private bool _fall = false;
        private float _time;
        public void Run()
        {
            foreach (var play in _playState)
            {
                ref var playState = ref _playState.Get1(play).PlayStates;
                if (playState == PlayStates.Menu)
                {
                    _ready = true;
                    return;
                }
            }

            if (_ready == false)
            {
                _time += Time.deltaTime;
            }
            foreach (var player in _player)
            {
                ref var physics = ref _player.Get2(player).Rigidbody;
                if (physics.velocity.y < 0)
                {
                    _fall = true;
                }
                ref var animator = ref _player.Get3(player).Animator;
                if (Mathf.Abs(physics.velocity.y) > 0.02f && _ready)
                {
                    _time = 0;
                    animator.SetTrigger("Jump");
                    _ready = false;
                    _fall = false;
                }
                else if (Mathf.Abs(physics.velocity.y) < 0.02f && !_ready && _fall)
                {
                    _ready = true;
                    animator.SetTrigger("Fall");
                }
            }
        }
    }
}
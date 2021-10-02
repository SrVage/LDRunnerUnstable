using System.Data.SqlTypes;
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class InputSystem:IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<TouchTime> _touch;
        private readonly EcsFilter<Aim> _aim;
        private float _touchPosition;
        private float _touchTime;
        private int _screenHeight;

        public InputSystem()
        {
            _screenHeight = Screen.height;
        }
        public void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
               _world.NewEntity().Get<TouchTime>();
               _touchTime = 0;
            }
            if (Input.GetMouseButton(0))
            {
                _touchTime += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (var touch in _touch)
                {
                    ref var entity = ref _touch.GetEntity(touch);
                        entity.Get<InputTouch>();
                        entity.Get<TouchTime>().Time = _touchTime<0.2f?_touchTime:0.2f;
                }
            }


            if (Input.GetMouseButtonDown(1))
            {
                _touchPosition = Input.mousePosition.y;
                _world.NewEntity().Get<Aim>();
            }
            if (Input.GetMouseButton(1))
            {
                foreach (var aim in _aim)
                {
                    ref var angle = ref _aim.Get1(aim).Angle;
                    angle = 60*(Input.mousePosition.y - _touchPosition)/_screenHeight;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                foreach (var aim in _aim)
                {
                    _aim.GetEntity(aim).Get<InputTouch>();
                }
            }
        }
    }
}
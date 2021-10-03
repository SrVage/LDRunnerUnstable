using System;
using System.ComponentModel.Design.Serialization;
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client.MonoBehs
{
    public class CanvasInit:MonoBehaviour
    {
        public Image Radiation;
        [SerializeField] private GameObject TTS;
        [SerializeField] private Slider _sliderAim;
        private EcsEntity _playState;
        private EcsEntity _aim;
        private bool _init = false;

        public void Init(EcsEntity playState, EcsWorld world)
        {
            _playState = playState;
            _aim = world.NewEntity();
            _aim.Get<Aim>();
            _init = true;
        }

        public void Tap()
        {
            ref var play = ref _playState.Get<PlayState>().PlayStates;
            play = PlayStates.EnterLevel;
            TTS.SetActive(false);
        }

        public void Update()
        {
            ref var play = ref _playState.Get<PlayState>().PlayStates;
            if (play == PlayStates.Menu && !TTS.activeSelf)
            {
                TTS.SetActive(true);
            }
        }

        public void ValueChanged()
        {
            if (!_init) return;
            ref var aim = ref _aim.Get<Aim>().Angle;
            aim = _sliderAim.value;
        }

        public void Fire()
        {
            _aim.Get<InputTouch>();
        }
    }
}
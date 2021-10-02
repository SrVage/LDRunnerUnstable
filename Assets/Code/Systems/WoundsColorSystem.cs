using System;
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;
using Renderer = Client.Components.Renderer;

namespace Client.Systems
{
    public class WoundsColorSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Radiation> _radiation;
        private readonly EcsFilter<Player, Renderer> _renderer;
        public void Run()
        {
            foreach (var rad in _radiation)
            {
                ref var radiation = ref _radiation.Get1(rad).RadiationValue;
                foreach (var rend in _renderer)
                {
                    ref var renderer = ref _renderer.Get2(rend).MeshRenderer;
                    var materials = renderer.materials;
                    materials[1].SetColor("_EmissionColor", Color.Lerp(Color.black, Color.red, radiation));
                }
            }
        }
    }
}
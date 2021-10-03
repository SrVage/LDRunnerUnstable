using System;
using Cinemachine;
using Client.Configs;
using Client.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private LevelGeneratorConfig _levelGeneratorConfig;
        [SerializeField] private GunConfig _gunConfig;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private UIConfig _uIConfig;
        [SerializeField] private PotionConfig _potionConfig;
        [SerializeField] private CinemachineVirtualCamera _camera;
        EcsWorld _world;
        EcsSystems _systems;

        void Start ()
        {
            Screen.orientation = ScreenOrientation.Landscape;
            Application.targetFrameRate = 60;
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your systems here, for example:
                .Add (new PlayerInitializeSystem())
                .Add(new LevelGeneratorSystem())
                .Add (new InputSystem())
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem(_camera, _world))
                .Add(new DestroyBlockSystem())
                .Add(new GunAimingSystem())
                .Add(new GunFireSystem())
                .Add(new RadiationIndicatorSystem())
                .Add(new RadiationSystem())
                .Add(new PotionCreateSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new WoundsColorSystem())
                .Add(new DeathSystem())
                .Add(new ExplosionSystem())
                .Add(new DestroySystem())
                .Add(new DestroyBulletSystem())
                
                // register one-frame components (order is important), for example:
                // .OneFrame<TestComponent1> ()
                // .OneFrame<TestComponent2> ()
                
                // inject service instances here (order doesn't important), for example:
                .Inject (_playerConfig)
                .Inject (_levelGeneratorConfig)
                .Inject(_gunConfig)
                .Inject(_bulletConfig)
                .Inject(_uIConfig)
                .Inject(_potionConfig)
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}
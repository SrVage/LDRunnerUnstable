using System;
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.MonoBehs
{
    public class PotionTrigger : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        private EcsWorld _world;

        public void Init(EcsWorld world)
        {
            _world = world;
        }
        private void OnTriggerStay(Collider other)
        {
            _rb.useGravity = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<PlayerViewRefs>())
            {
                _world.NewEntity().Get<GetPotion>();
                Destroy(gameObject);
            }
        }
    }
}
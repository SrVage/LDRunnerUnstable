using System.Collections.Generic;
using UnityEngine;

namespace Client.Services
{
    public class BulletPool
    {
        private Queue<GameObject> _bulletPool;
        private Transform _poolRoot;
        private GameObject _bulletPrefab;

        public BulletPool(GameObject bulletPrefab)
        {
            _bulletPool = new Queue<GameObject>();
            _bulletPrefab = bulletPrefab;
            _poolRoot = new GameObject("rootPool").transform;
            for (int i = 0; i < 10; i++)
            {
                var bulletGameObj = GameObject.Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity);
                ReturnToPool(bulletGameObj);
            }
        }

        public GameObject GetBullet()
        {
            if (_bulletPool.Count == 0)
            {
                var bulletGameObj = GameObject.Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity);
                ReturnToPool(bulletGameObj);
            }
            GameObject bullet = _bulletPool.Dequeue();
            bullet.SetActive(true);
            bullet.transform.parent = null;
            return bullet;
        }

        public void ReturnToPool(GameObject bullet)
        {
            bullet.transform.position = Vector3.zero;
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullet.transform.SetParent(_poolRoot);
            bullet.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }
    }
}
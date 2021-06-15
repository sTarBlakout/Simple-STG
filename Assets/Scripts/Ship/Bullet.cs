using System;
using Enemy;
using UnityEngine;

namespace Ship
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        
        private void FixedUpdate()
        {
            var pos = transform.position;
            pos.z += _speed;
            transform.position = pos;
        }
        
        public void Init(float damage, float speed)
        {
            TempContainer.Instance.MoveToContainer(gameObject);
            _speed = speed;
            _damage = damage;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var spaceObject = other.GetComponent<SpaceObject>();
            if (spaceObject != null)
            {
                spaceObject.Damage(_damage);
                Destroy(gameObject);
            }
        }
    }
}

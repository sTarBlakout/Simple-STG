using System;
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
            _speed = speed;
            _damage = damage;
        }
    }
}

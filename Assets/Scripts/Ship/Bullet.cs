using System;
using Enemy;
using Global;
using Player;
using UnityEngine;

namespace Ship
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        private bool _isEnemy;
        
        private void FixedUpdate()
        {
            var pos = transform.position;
            pos.z += _speed;
            transform.position = pos;
        }
        
        public void Init(float damage, float speed, bool isEnemy)
        {
            TempContainer.Instance.MoveToContainer(gameObject);
            _speed = speed;
            _damage = damage;
            _isEnemy = isEnemy;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_isEnemy)
            {
                var spaceObject = GameController.TryGetParentSpaceObject(other.transform);
                if (spaceObject != null)
                {
                    if (spaceObject.IsDestroyed) return;
                    spaceObject.Damage(_damage);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    other.transform.parent.parent.parent.GetComponent<PlayerController>().Damage(_damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}

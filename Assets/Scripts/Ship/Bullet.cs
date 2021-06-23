using System;
using Enemy;
using Global;
using Player;
using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Manages gatling gun bullet.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        private bool _isEnemy;
        
        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class.
        /// Is called every fixed "physics" frame after "Start".
        /// Moves bullet.
        /// </summary>
        private void FixedUpdate()
        {
            var pos = transform.position;
            pos.z += _speed;
            transform.position = pos;
        }
        
        /// <summary>
        /// Initializes bullet with damage and speed. Also defines if it was shot by enemy or player.
        /// </summary>
        public void Init(float damage, float speed, bool isEnemy)
        {
            TempContainer.Instance.MoveToContainer(gameObject);
            _speed = speed;
            _damage = damage;
            _isEnemy = isEnemy;
        }
        
        /// <summary>
        /// Unity event for checking if any collider touches this object.
        /// Deals damage to SpaceObject or PlayerShip depending on who it was shot from.
        /// </summary>
        /// <param name="other">Object with collider component which touched current object.</param>
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
                    other.GetComponent<PlayerController>().Damage(_damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}

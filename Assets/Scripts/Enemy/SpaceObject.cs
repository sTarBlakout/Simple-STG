using System;
using System.Collections;
using Player;
using Ship;
using UnityEngine;

namespace Enemy
{
    public abstract class SpaceObject : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] protected float speed = 1f;
        [SerializeField] protected float health = 1f;
        [SerializeField] protected float collisionDamage;

        [Header("Components")]
        [SerializeField] protected GameObject graphics;
        [SerializeField] protected GameObject explosionPrefab;

        public bool IsDestroyed { get; private set; }

        public virtual void Init(float healthMod)
        {
            health *= healthMod;
        }

        public virtual void Damage(float damage)
        {
            health -= damage;
            if (health <= 0) Die();
        }

        public virtual void Die()
        {
            IsDestroyed = true;
            graphics.GetComponent<MeshRenderer>().enabled = false;
            Instantiate(explosionPrefab, transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsDestroyed) return;
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.parent.parent.parent.GetComponent<PlayerController>().Damage(collisionDamage);
                Die();
            }
        }
    }
}

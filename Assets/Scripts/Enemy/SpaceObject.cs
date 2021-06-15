using System;
using Ship;
using UnityEngine;

namespace Enemy
{
    public abstract class SpaceObject : MonoBehaviour
    {
        [SerializeField] protected float speed = 1f;
        [SerializeField] protected float health = 1f;

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
            Destroy(gameObject);
        }
    }
}

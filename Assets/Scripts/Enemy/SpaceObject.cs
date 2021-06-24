using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Player;
using Ship;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Abstract class for each object wich will be spawned during waves.
    /// </summary>
    public abstract class SpaceObject : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] protected float speed = 1f;
        [SerializeField] public float health = 1f;
        [SerializeField] protected float collisionDamage;
        [SerializeField] protected int score;

        [Header("Components")]
        [SerializeField] protected GameObject graphics;
        [SerializeField] protected GameObject explosionPrefab;
        [SerializeField] protected AudioClip deathSound;

        public bool IsDestroyed { get; private set; }

        /// <summary>
        /// Virtual initialization method for the SpaceObject
        /// </summary>
        /// <param name="healthMod">Health modificator value</param>
        public virtual void Init(float healthMod)
        {
            health *= healthMod;
        }

        /// <summary>
        /// Virtual method for dealing damage to SpaceObject
        /// </summary>
        /// <param name="damage">Dealt damage value</param>
        public virtual void Damage(float damage)
        {
            if (IsDestroyed) return;
            health -= damage;
            if (health <= 0) Die();
        }

        /// <summary>
        /// Method which processes death of the object
        /// </summary>
        public virtual void Die()
        {
            IsDestroyed = true;
            if (deathSound != null && Camera.main != null)AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            if (graphics != null) graphics.GetComponent<MeshRenderer>().enabled = false;
            if (explosionPrefab != null) Instantiate(explosionPrefab, transform);
            if (GameController.instance != null) GameController.instance.AddScore(score);
        }

        /// <summary>
        /// Unity event for checking if any collider touches this object.
        /// Checks if PlayerShip tocuhed it, if so die and deal damage to PlayerShip.
        /// </summary>
        /// <param name="other">Object with collider component which touched current object.</param>
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (IsDestroyed) return;
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().Damage(collisionDamage);
                Die();
            }
        }
    }
}

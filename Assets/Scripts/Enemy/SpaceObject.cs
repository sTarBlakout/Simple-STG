using System;
using System.Collections;
using System.Collections.Generic;
using Global;
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
        [SerializeField] protected int score;

        [Header("Components")]
        [SerializeField] protected GameObject graphics;
        [SerializeField] protected GameObject explosionPrefab;
        [SerializeField] protected AudioClip deathSound;

        public bool IsDestroyed { get; private set; }

        public virtual void Init(float healthMod)
        {
            health *= healthMod;
        }

        public virtual void Damage(float damage)
        {
            if (IsDestroyed) return;
            health -= damage;
            if (health <= 0) Die();
        }

        public virtual void Die()
        {
            IsDestroyed = true;
            if (deathSound != null)AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            if (graphics != null) graphics.GetComponent<MeshRenderer>().enabled = false;
            if (explosionPrefab != null) Instantiate(explosionPrefab, transform);
            GameController.instance.AddScore(score);
        }

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

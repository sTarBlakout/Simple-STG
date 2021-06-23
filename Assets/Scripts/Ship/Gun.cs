using System;
using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Abstract class for guns.
    /// </summary>
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] private float fireRate;

        public float damage;
        protected float LastTimeShot = 0f;

        /// <summary>
        /// Abstract method for shooting.
        /// </summary>
        public abstract void Shoot();

        /// <summary>
        /// Checks if gun can shoot depending on fire rate.
        /// </summary>
        protected bool CanShoot()
        {
            return LastTimeShot + fireRate < Time.time;
        }
    }
}

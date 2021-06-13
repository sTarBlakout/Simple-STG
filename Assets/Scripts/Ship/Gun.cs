using System;
using UnityEngine;

namespace Ship
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] private float fireRate;
        [SerializeField] protected float damage;
        
        protected float LastTimeShot = 0f;

        public abstract void Shoot();

        protected bool CanShoot()
        {
            return LastTimeShot + fireRate < Time.time;
        }
    }
}

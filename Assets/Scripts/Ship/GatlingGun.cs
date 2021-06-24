using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Manages gatling gun.
    /// </summary>
    public class GatlingGun : Gun
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private bool isEnemy;

        /// <summary>
        /// Overrides abstract Shoot method. Shoots a bullet with sound.
        /// </summary>
        public override void Shoot()
        {
            if (!CanShoot()) return;
            
            LastTimeShot = Time.time;
            if (Camera.main != null) AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Init(damage, bulletSpeed, isEnemy);
        }
    }
}

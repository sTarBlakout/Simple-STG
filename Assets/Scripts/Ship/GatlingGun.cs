using UnityEngine;

namespace Ship
{
    public class GatlingGun : Gun
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;

        public override void Shoot()
        {
            if (!CanShoot()) return;
            
            LastTimeShot = Time.time;
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Init(damage, bulletSpeed);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using Ship;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Class which manages Player Ship behavior.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Parts")] 
        [SerializeField] private Transform rotatableBody;
        [SerializeField] private GameObject gunsHolder;

        [Header("Prefabs")] 
        [SerializeField] private GameObject explosion;
        [SerializeField] private AudioClip explosionSound;
        
        [Header("Stats")]
        [SerializeField] public float initHealth;
        [SerializeField] private float speed;
        [SerializeField] private float tiltSpeed;
        [SerializeField] private float tiltMaxAngle;
        
        [Header("Limitations")]
        [SerializeField] private float verticalLimit;
        [SerializeField] private float horizontalLimit;

        [HideInInspector] public float health;
        [HideInInspector] public bool isDead;

        public float CurrDamage => _guns?[0].damage ?? 0f;
        
        private List<Gun> _guns;
        private Joystick _joystick;

        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class
        /// Is called after "Awake" once when Monobehavior object is created.
        /// Does initialization logic for health. Also collects all attached guns to list.
        /// </summary>
        public void Init()
        {
            health = initHealth;
            _guns = gunsHolder.GetComponentsInChildren<Gun>().ToList();
            _joystick = FindObjectOfType<Joystick>();
        }
        
        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class.
        /// Is called every fixed "physics" frame after "Start".
        /// Calls methods for processing shooting and moving input.
        /// </summary>
        private void FixedUpdate()
        {
            if (isDead) return;
            
            ProcessMoveInput();
            ProcessShootInput();
        }

        /// <summary>
        /// Processes shooting input.
        /// </summary>
        private void ProcessShootInput()
        {
            Shoot();
        }

        /// <summary>
        /// Shoots from all attached guns.
        /// </summary>
        private void Shoot()
        {
            foreach (var gun in _guns) gun.Shoot();
        }

        /// <summary>
        /// Processes moving input.
        /// </summary>
        private void ProcessMoveInput()
        {
            var hasInput = false;

            if (_joystick.Horizontal < -0.5f)
            {
                Move(Direction.Left);
                hasInput = true;
            }
            if (_joystick.Horizontal > 0.5f)
            {
                Move(Direction.Right);
                hasInput = true;
            }
            if (_joystick.Vertical > 0.5f)
            {
                Move(Direction.Forward);
                hasInput = true;
            }
            if (_joystick.Vertical < -0.5f)
            {
                Move(Direction.Backward);
                hasInput = true;
            }

            if (!hasInput) Move(Direction.None);
        }

        /// <summary>
        /// Moves ship in passed direction.
        /// </summary>
        /// <param name="direction">Direction to move.</param>
        private void Move(Direction direction)
        {
            var currPos = transform.position;
            
            switch (direction)
            {
                case Direction.Forward: currPos.z += speed; break;
                case Direction.Backward: currPos.z -= speed; break;
                case Direction.Right: currPos.x += speed; break;
                case Direction.Left: currPos.x -= speed; break;
                case Direction.None: break;
            }

            if (currPos.z < verticalLimit && currPos.z > -verticalLimit && currPos.x < horizontalLimit && currPos.x > -horizontalLimit )
            {
                transform.position = currPos;
                Tilt(direction);
            }
            else Tilt(Direction.None);
        }

        /// <summary>
        ///  Tilts ship depending on passed moving direction.
        /// </summary>
        /// <param name="direction">Moving direction.</param>
        private void Tilt(Direction direction)
        {
            float rotateAngle;
            
            switch (direction)
            {
                case Direction.Right: rotateAngle = -tiltMaxAngle; break;
                case Direction.Left: rotateAngle = tiltMaxAngle; break;
                default: rotateAngle = 0f; break;
            }
            
            rotatableBody.rotation = Quaternion.Slerp(rotatableBody.rotation, Quaternion.Euler(0, 0, rotateAngle), tiltSpeed);
        }

        /// <summary>
        /// Processes player ship death.
        /// </summary>
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            if (Camera.main != null) AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
            Destroy(rotatableBody.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            if (GameController.instance != null) GameController.instance.FinishGame();
        }

        /// <summary>
        /// Processes incoming damage.
        /// </summary>
        /// <param name="damage">Damage value.</param>
        public void Damage(float damage)
        {
            health -= damage;
            if (health <= 0f) Die();
        }
        
        /// <summary>
        /// Processes incoming boost.
        /// </summary>
        /// <param name="damage">Damage boost value.</param>
        /// <param name="heal">Health boost value.</param>
        public void Boost(float damage, float heal)
        {
            health = Mathf.Min(health + heal, initHealth);
            foreach (var gun in _guns) gun.damage += damage;
        }
    }
}

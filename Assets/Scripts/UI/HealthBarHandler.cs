using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Manages health bar of the ship.
    /// </summary>
    public class HealthBarHandler : MonoBehaviour
    {
        [SerializeField] private Image greenArea;

        private PlayerController _player;

        /// <summary>
        /// Initializes health bar with player.
        /// </summary>
        public void Init(PlayerController player)
        {
            _player = player;
        }
    
        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class
        /// Is called every frame after "Start".
        /// Updates player health status on the health bar.
        /// </summary>
        public void Update()
        {
            if (_player == null) return;
            greenArea.fillAmount = _player.health / _player.initHealth;
        }
    }
}
 
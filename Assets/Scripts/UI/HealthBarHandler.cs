using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBarHandler : MonoBehaviour
    {
        [SerializeField] private Image greenArea;

        private PlayerController _player;

        public void Init(PlayerController player)
        {
            _player = player;
        }
    
        public void Update()
        {
            if (_player == null) return;
            greenArea.fillAmount = _player.health / _player.initHealth;
        }
    }
}
 
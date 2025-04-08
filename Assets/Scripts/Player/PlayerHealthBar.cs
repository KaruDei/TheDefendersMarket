using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class HelthBar : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerComponent _player;

        [Header("Helth Bar")]
        [SerializeField] private Image _healthBar;

        private void FixedUpdate()
        {
            //_healthBar.fillAmount = _player.Health / _player.MaxHealth;
        }
    }
}

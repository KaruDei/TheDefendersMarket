using UnityEngine;

namespace Player
{
    [RequireComponent (typeof (MovementController), typeof(InputManager))]
    public class Player : MonoBehaviour, IAttack, IHealth
    {
        [Header("Components")]
        [SerializeField] private InputManager _inputManager;

        [Header("Player Settings")]
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackDistance;
        [SerializeField] private LayerMask _attackLayerMask;

        [Header("Animator")]
        [SerializeField] private Animator _animator;

        private float _health;
        private bool _isAttack = false;
        private bool _isAlive = true;

        public InputManager InputManager => _inputManager;

        public float Damage => _damage;
        public float Health => _health;

        private void Start()
        {
            _health = _maxHealth;
        }

        private void FixedUpdate()
        {
            if (InputManager.Attack && !_isAttack) 
            {
                Attack();
            }
        }

        public void StartAttack() => _isAttack = true;
        public void EndAttack() => _isAttack = false;

        public void Attack()
        {
            if (!_isAlive) return;

            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                _animator.SetTrigger("Attack");
            }
        }

        public void CheckAttackHit()
        {
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _attackDistance, Color.red, 5f);
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, _attackDistance, _attackLayerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IHealth character))
                {
                    character.TakeDamage(Damage);
                }
            }
        }

        public void Heal(float value)
        {
            if (!_isAlive) return;
            
            _health = _health + value >= _maxHealth ? _maxHealth : _health + value;
        }

        public void TakeDamage(float value)
        {
            if (!_isAlive) return;

            _health = _health - value > 0 ? _health - value : 0;
           
            if (_health == 0) 
                Die();
        }

        public void Die()
        {
            if (!_isAlive) return;

            Debug.Log("Die");
            _isAlive = false;
        }
    }
}

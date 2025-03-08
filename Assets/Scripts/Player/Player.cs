using UnityEngine;

namespace Player
{
    [RequireComponent (typeof (MovementController), typeof(InputManager))]
    public class Player : Unit, IAttack
    {
        [Header("Components")]
        [SerializeField] private InputManager _inputManager;

        [Header("Player Settings")]
        [SerializeField] private float _damage;
        [SerializeField] private float _attackDistance;
        [SerializeField] private LayerMask _attackLayerMask;

        [Header("Animator")]
        [SerializeField] private Animator _animator;

        private bool _isAttack = false;
        private Camera _camera;

        public InputManager InputManager => _inputManager;

        public float Damage => _damage;

        private void Start()
        {
            _health = _maxHealth;
            _camera = Camera.main;
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
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, _attackDistance, _attackLayerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IHealth character))
                {
                    character.TakeDamage(Damage);
                }
            }
        }

        public override void Die()
        {
            _isAlive = false;
            Debug.Log("Die");
        }
    }
}

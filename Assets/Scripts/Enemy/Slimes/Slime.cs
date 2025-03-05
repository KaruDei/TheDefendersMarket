using UnityEngine;

namespace Enemy
{
    public class Slime : EnemyBase
    {
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackDistance;
        [SerializeField] private LayerMask _attackLayerMask;
         
        private float _health;

        public override float Damage => _damage;
        public override float Health => _health;

        private void Start()
        {
            _health = _maxHealth;
        }

        public override void Attack()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, _attackDistance, _attackLayerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IHealth character))
                {
                    character.TakeDamage(Damage);
                }
            }
        }
        
        public override void Heal(float value)
        {
            Debug.Log($"{GetType().Name} получил {value} здоровья");

            _health = _health + value >= _maxHealth ? _maxHealth : _health + value;
        }

        public override void TakeDamage(float value)
        {
            Debug.Log($"{GetType().Name} получил {value} урона");

            _health = _health - value > 0 ? _health - value : 0;

            if (_health == 0)
                Die();
        }

        public override void Die()
        {
            Destroy(gameObject);
        }
    }
}

using Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour, IAttack, IHealth, IAgent
    {
        [Header("Enemy Settings")]
        [SerializeField] protected float _maxHealth = 100f;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _attackDistance;
        [SerializeField] protected LayerMask _attackLayerMask;

        [Header("Agent")]
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected float _stopDistance = 2.0f;
        [SerializeField] protected List<Transform> _points = new ();

        protected float _health;
        [SerializeField] protected Transform _target;

        private void Start()
        {
            _health = _maxHealth;
        }

        public float Damage => _damage;
        public float MaxHealth => _maxHealth;
        public float Health => _health;

        public abstract void Attack();

        public virtual void Heal(float value)
        {
            _health = _health + value >= _maxHealth ? _maxHealth : _health + value;
        }

        public virtual void TakeDamage(float value)
        {
            _health = _health - value > 0 ? _health - value : 0;

            if (_health == 0)
                Die();
        }

        public abstract void Die();

        public virtual void Move()
        {


            GetDestination();
            SetDestination();
        }

        //1. GetDestination
        //  1.1. Проверка на игрока, юнита или врат
        //  1.2. Поиск точки маршрута

        public virtual void GetDestination()
        {
            
        }

        // 2. SetDestination
        //  2.1. Установка цели

        public virtual void SetDestination()
        {
            //if (_target == null) return;

            // Движение к цели
                _agent.SetDestination(_target.position);

            // Остановка движения и слежение за целью
            if (_agent.remainingDistance < 2.5f)
            {
                _agent.isStopped = true;

                Vector3 lookDirection = (_target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z), Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
            else
                _agent.isStopped = false;

            // Отталкивание агента от цели
            float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
            if (distanceToTarget < 1.5f)
            {
                Vector3 moveDirection = (transform.position - _target.transform.position).normalized;
                _agent.Move(moveDirection * (1.5f - distanceToTarget));
            }
        }
    }
}

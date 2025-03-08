using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitAgentBase : Unit, IAttack, IAgent
{ 
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackDistance;
    [SerializeField] protected float _attackDelay;
    [SerializeField] protected LayerMask _attackLayerMask;

    [Header("Agent")]
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected float _stopDistance = 2.0f;
    [SerializeField] protected float _playerFollowDistance = 10.0f;

    // Targets
    protected Transform _baseTarget;
    protected Transform _player;
    protected Transform _currentTarget;

    // Attack
    protected bool _isCanAttack = true;
    protected bool _isAttack = false;
    protected float _attackTime = 0f;
    protected IHealth _healthHit;

    public float Damage => _damage;

    public virtual void Attack()
    {
        if (!_isCanAttack && !_isAttack)
        {
            _attackTime += Time.fixedDeltaTime;
            if (_attackTime >= _attackDelay)
            {
                _attackTime = 0;
                _isCanAttack = true;
            }
        }

        if (_isCanAttack && !_isAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position + transform.up * _agent.height / 2f, transform.forward, out hitInfo, _attackDistance, _attackLayerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IHealth healthHit))
                {
                    _healthHit = healthHit;
                    // запуск анимации
                    // вызов метода нанесения урона в аниматоре
                    StartAttack();
                    AttackHealthHit();
                    EndAttack();
                }
            }
            //else
            //{
            //    Debug.DrawRay(transform.position + transform.up * _agent.height / 2f, transform.forward * _attackDistance, Color.yellow, 2f);
            //}
        }
    }

    protected virtual void AttackHealthHit()
    {
        //Debug.Log("Attack");
        if (_healthHit == null) return;
        if (_isAttack)
        {
            //Debug.Log(_healthHit.GetType().Name + " - Получает " + Damage + " урона");
            _healthHit.TakeDamage(Damage);
            _healthHit = null;
        }
    }

    public virtual void StartAttack()
    {
        _isAttack = true;
        _isCanAttack = false;
    }

    public virtual void EndAttack()
    {
        _isAttack = false;
    }

    public virtual void SetTarget(Transform target)
    {
        _baseTarget = target;
    }

    public virtual void SetPlayer(Transform player)
    {
        _player = player;
    }

    public virtual void Move()
    {
        GetTarget();
        AgentStopAndTrackingTarget();
        PushingAwayFromPlayer();
    }

    //1. GetDestination
    //  1.1. Проверка на игрока, юнита или врат
    //  1.2. Поиск точки маршрута

    public virtual void GetTarget()
    {
        Transform newTarget = _baseTarget;

        if (Vector3.Distance(transform.position, _player.position) < _playerFollowDistance)
            newTarget = _player;

        if (newTarget != _currentTarget || newTarget == _player)
        {
            _currentTarget = newTarget;
            SetDestination();
        }
    }

    // 2. SetDestination
    //  2.1. Установка цели

    public virtual void SetDestination()
    {
        if (_currentTarget == null) return;

        //Debug.Log("Set Destination: " + _currentTarget.name);
        _agent.SetDestination(_currentTarget.position);
    }

    private void AgentStopAndTrackingTarget()
    {
        if (_currentTarget == null) return;

        if (_agent.remainingDistance < 2.5f)
        {
            _agent.isStopped = true;

            Vector3 lookDirection = (_currentTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            if (!_isAttack)
            {
                _agent.isStopped = false;
            }
        }
    }

    private void PushingAwayFromPlayer ()
    {
        if (_player == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);
        if (distanceToTarget < 1.5f)
        {
            Vector3 moveDirection = (transform.position - _player.transform.position).normalized;
            _agent.Move(moveDirection * (1.5f - distanceToTarget));
        }
    }
}

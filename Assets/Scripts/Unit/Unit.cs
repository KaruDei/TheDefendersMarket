using UnityEngine;

public abstract class Unit : MonoBehaviour, IHealth
{
    [Header("Enemy Settings")]
    [SerializeField] protected float _maxHealth = 100f;
    
    protected bool _isAlive = true;

    public bool IsAlive => _isAlive;

    // Properties
    protected float _health;

    protected virtual void Start()
    {
        _health = _maxHealth;
    }

    public float MaxHealth => _maxHealth;
    public float Health => _health;

    public virtual void Heal(float value)
    {
        if (!_isAlive) return;

        _health = _health + value >= _maxHealth ? _maxHealth : _health + value;
    }

    public virtual void TakeDamage(float value)
    {
        if (!_isAlive) return;

        _health = _health - value > 0 ? _health - value : 0;

        if (_health == 0)
            Die();
    }

    public virtual void Die()
    {
        if (!_isAlive) return;
        Destroy(gameObject);
    }
}

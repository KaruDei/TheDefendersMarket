using UnityEngine;

public class EntityStats : MonoBehaviour, IHealth
{
    [SerializeField] protected EntityData _entityData;

    [Header("Health")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    [SerializeField] private VoidGameEvent _onDeath;
    [SerializeField] private FloatGameEvent _onHealthChanged;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    protected virtual void Start()
    {
        _maxHealth = _entityData.MaxHealth;
        _currentHealth = _maxHealth;
        _onHealthChanged?.Raise(_currentHealth / _maxHealth);
    }

    public virtual void TakeDamage(float value)
    {
        _currentHealth -= value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        _onHealthChanged?.Raise(_currentHealth / _maxHealth);

        if (_currentHealth <= 0)
            Die();
    }

    public virtual void Heal(float value)
    {
        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _onHealthChanged?.Raise(_currentHealth / _maxHealth);
    }

    public virtual void Die()
    {
        _onDeath?.Raise();
        // Здесь можно делать destroy, отключение логики, анимации и т.д.
        Debug.Log($"{gameObject.name} died.");
    }
}

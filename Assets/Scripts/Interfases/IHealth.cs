public interface IHealth
{
    float MaxHealth { get; }
    float CurrentHealth { get; }
    void Heal(float value);
    void TakeDamage(float value);
    void Die();
}

public interface IHealth
{
    float MaxHealth { get; }
    float Health { get; }
    void Heal(float value);
    void TakeDamage(float value);
    void Die();
}

public interface IHealth
{
    float Health { get; }
    void Heal(float value);
    void TakeDamage(float value);
    void Die();
}

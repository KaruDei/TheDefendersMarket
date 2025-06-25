public interface IStamina
{
    float MaxStamina { get; }
    float CurrentStamina { get; }
    void UseStamina(float value);
    void RecoverStamina(float value);
}

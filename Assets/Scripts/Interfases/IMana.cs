public interface IMana
{
    float MaxMana { get; }
    float CurrentMana { get; }
    void UseMana(float value);
    void RecoverMana(float value);
}

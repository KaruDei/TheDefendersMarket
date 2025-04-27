using UnityEngine;

public abstract class WeaponItem : ScriptableItem
{
    [Header("Weapon Damage")]
    [SerializeField, Tooltip("Урон оружия")] private float _damage;

    // Возможно нужно будет реализовать этот пораметр для каждого типа оружия
    // (для меча и магического посоха - дальность атаки, а для лука - "???сила натяжения???")
    [Header("Weapon Attack Distance")]
    [SerializeField, Tooltip("Дальность атаки оружия")] private float _attackDistance;

    // Или контроллировать через анимации. Хз вообще　・＿・
    [Header("Weapon Attack Recovery Time")]
    [SerializeField, Tooltip("Время востановления атаки")] private float _attackRecoveryTime;

    public float Damage => _damage;
    public float AttackDistance => _attackDistance;
    public float AttackRecoveryTime => _attackRecoveryTime;
}

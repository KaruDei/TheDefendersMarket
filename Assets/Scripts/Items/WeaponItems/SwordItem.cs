using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Data/Item/Weapon/Sword")]
public class SwordItem : WeaponItem
{
    [Header("Sword Attack Range")]
    [SerializeField, Tooltip("Диапазон атаки в градусах, где 180 градусов это полный оборот")] private float _attackRange;

    [Header("Sword Attack Repulsive Force")]
    [SerializeField, Tooltip("Отталкивающая сила оружия")] private float _attackRepulsiveForce;

    [Header("Sword Attack Stamina Cost")]
    [SerializeField, Tooltip("Затраты выносливости для атаки")] private float _attackStaminaCost;

    public float AttackRange => _attackRange;
    public float AttackRepulsiveForce => _attackRepulsiveForce;
    public float AttackStaminaCost => _attackStaminaCost;
}

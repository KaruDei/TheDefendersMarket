using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Data/Item/Weapon/Sword")]
public class SwordItem : WeaponItem
{
    [Header("Sword Attack Range")]
    [SerializeField, Tooltip("�������� ����� � ��������, ��� 180 �������� ��� ������ ������")] private float _attackRange;

    [Header("Sword Attack Repulsive Force")]
    [SerializeField, Tooltip("������������� ���� ������")] private float _attackRepulsiveForce;

    [Header("Sword Attack Stamina Cost")]
    [SerializeField, Tooltip("������� ������������ ��� �����")] private float _attackStaminaCost;

    public float AttackRange => _attackRange;
    public float AttackRepulsiveForce => _attackRepulsiveForce;
    public float AttackStaminaCost => _attackStaminaCost;
}

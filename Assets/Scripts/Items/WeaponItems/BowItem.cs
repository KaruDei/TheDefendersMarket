using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "Data/Item/Weapon/Bow")]
public class BowItem : WeaponItem
{
    [Header("Bow Arrow Prefab")]
    [SerializeField, Tooltip("������� ������ ������")] private GameObject _arrowPrefab;

    [Header("Bow Arrow Alive Time")]
    [SerializeField, Tooltip("����� ����� ������ ����� ���������")] private float _arrowAliveTime;

    [Header("Bow Attack Stamina Cost")]
    [SerializeField, Tooltip("������� ������������ ��� �����")] private float _attackStaminaCost;

    public GameObject ArrowPrefab => _arrowPrefab;
    public float ArrowAliveTime => _arrowAliveTime;
    public float ArrowAttackStaminaCost => _attackStaminaCost;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Magic Staff", menuName = "Data/Item/Weapon/Magic Staff")]
public class MagicStaffItem : WeaponItem
{
    [Header("Magic Staff Magic Prefab")]
    [SerializeField, Tooltip("������� ������ ����������� �������")] private GameObject _magicPrefab;

    [Header("Magic Staff Attack Mana Cost")]
    [SerializeField, Tooltip("������� ���� ��� �����")] private float _attackManaCost;

    public GameObject MagicPrefab => _magicPrefab;
    public float AttackManaCost => _attackManaCost;
}

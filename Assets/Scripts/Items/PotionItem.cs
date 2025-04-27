using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Data/Item/Potion")]
public class PotionItem : ScriptableItem
{
    [Header("Potion Settings")]
    [SerializeField, Tooltip("������������� ��� ������������� ������� ����� �� ��������")]private float _health;
    [SerializeField, Tooltip("������������� ��� ������������� ������� ����� �� ����")]private float _mana;
    [SerializeField, Tooltip("������������� ��� ������������� ������� ����� �� �������")]private float _stamina;

    public float Health => _health;
    public float Mana => _mana;
    public float Stamina => _stamina;
}

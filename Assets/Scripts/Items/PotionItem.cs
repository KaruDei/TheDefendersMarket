using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Data/Item/Potion")]
public class PotionItem : ScriptableItem
{
    [Header("Potion Settings")]
    [SerializeField, Tooltip("Положительное или отрицательное влияние зелья на здоровье")]private float _health;
    [SerializeField, Tooltip("Положительное или отрицательное влияние зелья на ману")]private float _mana;
    [SerializeField, Tooltip("Положительное или отрицательное влияние зелья на стамину")]private float _stamina;

    public float Health => _health;
    public float Mana => _mana;
    public float Stamina => _stamina;
}

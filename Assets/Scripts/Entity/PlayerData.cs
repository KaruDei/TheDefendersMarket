using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Entity/PlayerStats")]
public class PlayerData : EntityData
{
    [Header("Mana")]
    [SerializeField] private float _maxMana;
    public float MaxMana => _maxMana;

    [Header("Stamina")]
    [SerializeField] private float _maxStamina;
    public float MaxStamina => _maxStamina;
}

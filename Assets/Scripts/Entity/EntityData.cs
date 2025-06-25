using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Entity/EntityStats")]
public class EntityData : ScriptableObject
{
    [Header("Health")]
    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;
}

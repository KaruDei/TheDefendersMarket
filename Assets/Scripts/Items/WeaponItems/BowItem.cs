using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "Data/Item/Weapon/Bow")]
public class BowItem : WeaponItem
{
    [Header("Bow Arrow Prefab")]
    [SerializeField, Tooltip("Игровой объект стрелы")] private GameObject _arrowPrefab;

    [Header("Bow Arrow Alive Time")]
    [SerializeField, Tooltip("Время жизни стрелы после попадания")] private float _arrowAliveTime;

    [Header("Bow Attack Stamina Cost")]
    [SerializeField, Tooltip("Затраты выносливости для атаки")] private float _attackStaminaCost;

    public GameObject ArrowPrefab => _arrowPrefab;
    public float ArrowAliveTime => _arrowAliveTime;
    public float ArrowAttackStaminaCost => _attackStaminaCost;
}

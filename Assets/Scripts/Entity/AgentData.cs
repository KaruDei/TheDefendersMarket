using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Entity/AgentStats")]
public class AgentData : EntityData
{
    [Header("Attack")]
    [SerializeField] private bool _canAttack = true;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRange;
    public bool CanAttack => _canAttack;
    public float AttackDamage => _attackDamage;
    public float AttackRange => _attackRange;
}

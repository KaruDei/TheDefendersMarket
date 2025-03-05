using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour, IAttack, IHealth
    {
        public abstract float Damage { get; }
        public abstract float Health { get; }

        public abstract void Attack();
        public abstract void Heal(float value);
        public abstract void TakeDamage(float value);
        public abstract void Die();
    }
}

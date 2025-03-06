using UnityEngine;

namespace Enemy
{
    public class Slime : EnemyBase
    {
        public override void Attack()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, _attackDistance, _attackLayerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IHealth character))
                {
                    character.TakeDamage(Damage);
                }
            }
        }
    
        public override void Die()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            Move();
        }
    }
}

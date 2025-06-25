using UnityEngine;

public class MagicTower : DefenderStructureBase
{
    [SerializeField] private float _damage = 15f;
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _attackCooldown = 1f;    
    [SerializeField] private LayerMask _enemyLayerMask;    
    //[SerializeField] private GameObject projectilePrefab;  
    [SerializeField] private Transform _firePoint;

    [SerializeField] private float _cooldownTimer = 0f;
    [SerializeField] private Transform _target;

    private void FixedUpdate()
    {
        _cooldownTimer -= Time.fixedDeltaTime;

        // Поиск ближайшего врага в радиусе
        FindClosestEnemy();

        // Если есть цель и кулдаун истек, атакуем
        if (_target != null && _cooldownTimer <= 0f)
        {
            Attack();
            _cooldownTimer = _attackCooldown;
        }
    }

    private void FindClosestEnemy()
    {
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        Collider[] hits = Physics.OverlapSphere(transform.position, _range, _enemyLayerMask);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IHealth health))
            {
                //Debug.Log($"Find: {hit.name}");
                float distanceToEnemy = Vector3.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = hit.transform;
                }
            }
        }

        _target = nearestEnemy;
    }

    private void Attack()
    {

        if (_target != null && Physics.Raycast(_firePoint.position, Vector3.Normalize(_target.position - _firePoint.position) * _range, out RaycastHit hit, _range + 5f, _enemyLayerMask)) 
        {
            //Debug.Log($"Attack: {hit.collider.name}");
            if (hit.collider.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_damage);
            }
        }
    }

    public override void UpgradeStructure()
    {
        
    }
}

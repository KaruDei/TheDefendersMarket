using UnityEngine;

public class Wall : DefenderStructureBase
{
    public override void UpgradeStructure()
    {
        
    }

    public override void Die()
    {
        if (!_isAlive) return;
        EventBus.PublishBuildingDestroyed(this);
        Destroy(gameObject);
    }
}

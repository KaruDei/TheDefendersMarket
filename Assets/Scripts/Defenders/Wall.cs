using UnityEngine;

public class Wall : DefenderStructureBase
{
    public override void UpgradeStructure()
    {
        
    }

    public override void Die()
    {
        base.Die();
        EventBus.PublishBuildingDestroyed(this);
    }
}

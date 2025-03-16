using UnityEngine;

public abstract class DefenderStructureBase : Unit, IUpgrade
{
    public abstract void UpgradeStructure();

    public override void Die()
    {
        if (!_isAlive) return;
        EventBus.PublishBuildingDestroyed(this);
        Destroy(gameObject);
    }
}

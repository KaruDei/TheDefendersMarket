using UnityEngine;

public abstract class DefenderStructureBase : Unit, IUpgrade
{
    public abstract void UpgradeStructure();

    public override void Die()
    {
        base.Die();
        EventBus.PublishBuildingDestroyed(this);
    }
}

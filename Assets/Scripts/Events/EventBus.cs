using Player;
using System;

public static class EventBus
{
    public static event Action<PlayerController> OnPlayerDie;
    public static event Action<DefenderStructureBase> OnBuildingDestroyed;
    public static event Action<Gate> OnGateDestroyed;
    public static event Action<UnitAgentBase> OnUnitAgentDie;

    public static void PublishPlayerDie(PlayerController player)
    {
        OnPlayerDie?.Invoke(player);
    }

    public static void PublishBuildingDestroyed(DefenderStructureBase building)
    {
        OnBuildingDestroyed?.Invoke(building);
    }

    public static void PublishGateDestroyed(Gate gate)
    {
        OnGateDestroyed?.Invoke(gate);
    }

    public static void PublishUnitAgentDie(UnitAgentBase unitAgent)
    {
        OnUnitAgentDie?.Invoke(unitAgent);
    }
}

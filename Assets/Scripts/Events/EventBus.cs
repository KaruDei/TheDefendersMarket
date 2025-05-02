using System;

public static class EventBus
{
    public static event Action<PlayerComponent> OnPlayerDie;
    public static event Action<DefenderStructureBase> OnBuildingDestroyed;
    public static event Action<Gate> OnGateDestroyed;
    public static event Action<UnitAgentBase> OnUnitAgentDie;
    public static event Action<Inventory> OnInventoryIsFull;
    public static event Action<Item> OnAddItemToIntentory;
    public static event Action OnInteraction;
    public static event Action<Item> OnItemUse;

    public static void PublishPlayerDie(PlayerComponent player)
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

    public static void PublishInventoryIsFull(Inventory inventory)
    {
        OnInventoryIsFull?.Invoke(inventory);
    }

    public static void PublishInteraction()
    {
        OnInteraction?.Invoke();
    }

    public static void PublishAddItemToInventory(Item item)
    {
        OnAddItemToIntentory?.Invoke(item);
    }

    public static void PublishItemUse(Item item)
    {
        OnItemUse?.Invoke(item);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private List<Slot> _slots;

    public List<Slot> Slots => _slots;

    /// <summary>
    /// Метод инвентаря, который добавляет предметы в слоты инвентаря.
    /// </summary>
    /// <param name="scriptableItem">Тип предмета.</param>
    /// <param name="count">Количество добавляемых предметов.</param>
    /// <returns>Количество оставшихся предметов.</returns>
    public int AddItem(ScriptableItem scriptableItem, int count = 1)
    {
        if (count < 0) count = Mathf.Abs(count);
        if (scriptableItem == null || count == 0) return 0;

        foreach (Slot slot in _slots)
        {
            if (count == 0) break;
            if (slot.ScriptableItem == scriptableItem)
            {
                int available = slot.GetAvailableQuantity();
                if (count - available >= 0)
                {
                    slot.AddItem(available);
                    count -= available;
                }
                else
                {
                    slot.AddItem(count);
                    count = 0;
                }
            }
        }

        foreach (Slot slot in _slots)
        {
            if (count == 0) break;
            if (slot.ScriptableItem == null)
            {
                if (count - scriptableItem.MaxSlotCapacity >= 0)
                {
                    slot.SetSlot(scriptableItem, scriptableItem.MaxSlotCapacity);
                    count -= scriptableItem.MaxSlotCapacity;
                }
                else
                {
                    slot.SetSlot(scriptableItem, count);
                    count = 0;
                }
            }
        }

        _inventoryUI.UpdateInventoryUISlotsInfo();

        if (count > 0)
            EventBus.PublishInventoryIsFull(this);
        return count;
    }

    /// <summary>
    /// Метод инвентаря, который удаляет предметы из слотов инвентаря.
    /// </summary>
    /// <param name="scriptableItem">Тип предмета.</param>
    /// <param name="count">Количество предметов для удаления</param>
    public bool RemoveItem(ScriptableItem scriptableItem, int count = 1)
    {
        if (count < 0) count = Mathf.Abs(count);
        if (scriptableItem == null || count == 0) return false;

        if (!CanRemoveItem(scriptableItem, count)) return false;
        
        foreach (Slot slot in _slots)
        {
            if (count == 0) break;
            if (slot.ScriptableItem == scriptableItem)
            {
                if (count - slot.Capacity >= 0)
                {
                    slot.RemoveItem(count - slot.Capacity);
                    count -= slot.Capacity;
                }
                else
                {
                    slot.RemoveItem(count);
                    count = 0;
                }
            }
        }

        _inventoryUI.UpdateInventoryUISlotsInfo();

        return count == 0;
    }

    /// <summary>
    /// Метод инвентаря который проверяет наличие предмета в инвентаре.
    /// </summary>
    /// <param name="scriptableItem">Тип предмета.</param>
    /// <param name="count">Количество предметов.</param>
    /// <returns></returns>
    public bool CanRemoveItem(ScriptableItem scriptableItem, int count = 1)
    {
        if (count < 0) count = Mathf.Abs(count);
        if (scriptableItem == null || count == 0) return false;

        foreach (Slot slot in _slots)
        {
            if (count == 0) break;
            if (slot.ScriptableItem == scriptableItem) 
            {
                if (count - slot.Capacity >= 0)
                    count -= slot.Capacity;
                else
                    count = 0;
            }
        }

        return count == 0;
    }

    private void AddItemToInventory(Item item)
    {
        if (item == null || item.ScriptableItem == null) return;

        int count = AddItem(item.ScriptableItem, item.ItemCount);

        if (count == 0)
            item.RemoveItem(item.ItemCount);
        else if (count > 0)
            item.RemoveItem(item.ItemCount - count);
        else
            Debug.LogError("Count < 0");
    }

    private void OnEnable()
    {
        EventBus.OnAddItemToIntentory += AddItemToInventory;
    }

    private void OnDisable()
    {
        EventBus.OnAddItemToIntentory -= AddItemToInventory;
    }
}

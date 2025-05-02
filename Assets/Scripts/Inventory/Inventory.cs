using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField, Tooltip("Компонент IntentoryUI")] private InventoryUI _inventoryUI;
    
    [Header("Slots")]
    [SerializeField, Tooltip("Слоты")] private List<Slot> _slots = new();

    public List<Slot> Slots => _slots;

    /// <summary>
    /// Метод, добавляющий предмет в инвентарь
    /// </summary>
    /// <param name="item">Ссылка на объект типа Item</param>
    public void AddItem(Item item)
    {
        if (item == null) return;

        List<Slot> slots = _slots.FindAll(slot => slot.Item == item.ScriptableItem && slot.GetAvailableQuantity() > 0);
        foreach (Slot slot in slots)
        {
            int availableCount = slot.GetAvailableQuantity();
            if (item.ItemCount <= availableCount)
            {
                slot.AddItem(item.ItemCount);
                item.RemoveItem(item.ItemCount);
                _inventoryUI.UpdateUISlotsInfo();
                return;
            }
            else
            {
                slot.AddItem(availableCount);
                item.RemoveItem(availableCount);
                continue;
            }
        }

        if (item.ItemCount > 0)
        {
            slots = _slots.FindAll(slot => slot.Item == null);
            int availableCount = item.ScriptableItem.MaxSlotCapacity;
            
            foreach (Slot slot in slots)
            {
                if (item.ItemCount <= availableCount)
                {
                    slot.SlotSetup(item.ScriptableItem, item.ItemCount);
                    item.RemoveItem(item.ItemCount);
                    _inventoryUI.UpdateUISlotsInfo();
                    return;
                }
                else
                {
                    slot.SlotSetup(item.ScriptableItem, item.ItemCount - availableCount);
                    item.RemoveItem(item.ItemCount - availableCount);
                    continue;
                }
            }
        }

        _inventoryUI.UpdateUISlotsInfo();
    }

    /// <summary>
    /// Метод, добавляющий предмет в инвентарь
    /// </summary>
    /// <param name="item">Предмет типа Scriptable Item</param>
    /// <param name="count">Количество добавляемых предметов</param>
    public void AddItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return;

        if (!CanAddItem(item, count)) return;

        List<Slot> slots = _slots.FindAll(slot => slot.Item == item && slot.GetAvailableQuantity() > 0);
        foreach (Slot slot in slots)
        {
            int availableCount = slot.GetAvailableQuantity();
            if (count <= availableCount)
            {
                slot.AddItem(count);
                count = 0;
                return;
            }
            else
            {
                slot.AddItem(availableCount);
                count -= availableCount;
                continue;
            }
        }

        if (count > 0)
        {
            slots = _slots.FindAll(slot => slot.Item == null);
            int availableCount = item.MaxSlotCapacity;

            foreach (Slot slot in slots)
            {
                if (count <= availableCount)
                {
                    slot.SlotSetup(item, count);
                    count = 0;
                    _inventoryUI.UpdateUISlotsInfo();
                    return;
                }
                else
                {
                    slot.SlotSetup(item, count - availableCount);
                    count -= availableCount;
                    continue;
                }
            }
        }

        _inventoryUI.UpdateUISlotsInfo();
    }

    /// <summary>
    /// Метод удаляющий предмет из инвентаря
    /// </summary>
    /// <param name="item">Предмет типа Scriptable Item</param>
    /// <param name="count">Количество удаляемых предметов</param>
    public void RemoveItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return;

        if (!CanRemoveItem(item, count)) return;

        List<Slot> slots = _slots.FindAll(slot => slot.Item == item);

        foreach (Slot slot in slots)
        {
            if (count <= slot.ItemsCount)
            {
                slot.RemoveItem(count);
                count = 0;
                _inventoryUI.UpdateUISlotsInfo();
                return;
            }
            else
            {
                count -= slot.ItemsCount;
                slot.RemoveItem(slot.ItemsCount);
                continue;
            }
        }

        _inventoryUI.UpdateUISlotsInfo();
    }

    /// <summary>
    /// Метод проверяющий возможность добавления предметов в инвентарь
    /// </summary>
    /// <param name="item">Предмет типа Scriptable Item</param>
    /// <param name="count">Количество добавляемых предметов</param>
    /// <returns>Возвращает значение типа bool указывающее возможность добавления предметов</returns>
    public bool CanAddItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return false;

        List<Slot> slots = _slots.FindAll(slot => slot.Item == item && slot.GetAvailableQuantity() > 0);
        foreach (Slot slot in slots)
        {
            int availableCount = slot.GetAvailableQuantity();
            if (count <= availableCount)
            {
                count = 0;
                return true;
            }
            else
            {
                count -= availableCount;
                continue;
            }
        }

        if (count > 0)
        {
            slots = _slots.FindAll(slot => slot.Item == null);
            int availableCount = item.MaxSlotCapacity;

            foreach (Slot slot in slots)
            {
                if (count <= availableCount)
                {
                    count = 0;
                    return true;
                }
                else
                {
                    count -= availableCount;
                    continue;
                }
            }
        }

        return count == 0;
    }

    /// <summary>
    /// Метод проверяющий возможность удаления предметов из инвентаря
    /// </summary>
    /// <param name="item">Предмет типа Scriptable Item</param>
    /// <param name="count">Количество удаляемых предметов</param>
    /// <returns>Возвращает значение типа bool указывающее возможность удаления предметов</returns>
    public bool CanRemoveItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return false;

        List<Slot> slots = _slots.FindAll(slot => slot.Item == item);

        foreach (Slot slot in slots)
        {
            if (count <= slot.ItemsCount)
            {
                count = 0;
                return true;
            }
            else
            {
                count -= slot.ItemsCount;
                continue;
            }
        }

        return count == 0;
    }
}
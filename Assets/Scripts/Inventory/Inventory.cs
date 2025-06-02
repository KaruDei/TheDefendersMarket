using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField, Tooltip("Компонент IntentoryUI")] private InventoryUI _inventoryUI;
    
    [Header("Slots")]
    [SerializeField, Tooltip("Слоты")] private List<Slot> _slots = new();

    private SlotUI _selectedSlot;

    public List<Slot> Slots => _slots;

    private void Start()
    {
        _inventoryUI.UpdateUISlotsInfo();
    }

    public void AddItem(Item item)
    {
        if (item == null) return;

        TryAddToExistingSlots(item);
        TryAddToEmptySlots(item);

        _inventoryUI.UpdateUISlotsInfo();
    }

    public void AddItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return;
        if (!CanAddItem(item, count)) return;

        TryAddToExistingSlots(item, ref count);
        TryAddToEmptySlots(item, ref count);

        _inventoryUI.UpdateUISlotsInfo();
    }

    public void RemoveItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return;
        if (!CanRemoveItem(item, count)) return;

        foreach (Slot slot in _slots)
        {
            if (slot.Item != item) continue;

            int amountToRemove = Mathf.Min(slot.ItemsCount, count);
            slot.RemoveItem(amountToRemove);
            count -= amountToRemove;

            if (count == 0) break;
        }

        _inventoryUI.UpdateUISlotsInfo();
    }

    public bool CanAddItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return false;

        int space = 0;

        foreach (Slot slot in _slots)
        {
            if (slot.Item == item)
                space += slot.GetAvailableQuantity();
        }

        foreach (Slot slot in _slots)
        {
            if (slot.Item == null)
                space += item.MaxSlotCapacity;
        }

        return count <= space;
    }

    public bool CanRemoveItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return false;

        int total = 0;

        foreach (Slot slot in _slots)
        {
            if (slot.Item == item)
                total += slot.ItemsCount;
        }

        return count <= total;
    }

    private void TryAddToExistingSlots(Item item)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item != item.ScriptableItem || slot.GetAvailableQuantity() <= 0) continue;

            int amountToAdd = Mathf.Min(item.ItemCount, slot.GetAvailableQuantity());
            slot.AddItem(amountToAdd);
            item.RemoveItem(amountToAdd);

            if (item == null || item.ItemCount == 0) break;
        }
    }

    private void TryAddToExistingSlots(ScriptableItem item, ref int count)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item != item || slot.GetAvailableQuantity() <= 0) continue;

            int amountToAdd = Mathf.Min(count, slot.GetAvailableQuantity());
            slot.AddItem(amountToAdd);
            count -= amountToAdd;

            if (count == 0) break;
        }
    }

    private void TryAddToEmptySlots(Item item)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item != null) continue;

            int amountToAdd = Mathf.Min(item.ItemCount, item.ScriptableItem.MaxSlotCapacity);
            slot.SlotSetup(item.ScriptableItem, amountToAdd);
            item.RemoveItem(amountToAdd);

            if (item == null || item.ItemCount == 0) break;
        }
    }

    private void TryAddToEmptySlots(ScriptableItem item, ref int count)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item != null) continue;

            int amountToAdd = Mathf.Min(count, item.MaxSlotCapacity);
            slot.SlotSetup(item, amountToAdd);
            count -= amountToAdd;

            if (count == 0) break;
        }
    }

    public void SelectSlot(SlotUI slotUI)
    {
        if (slotUI == null) return;

        if (_selectedSlot == slotUI)
        {
            _selectedSlot.SetSelect(false);
            _selectedSlot = null;
            return;
        }

        if (_selectedSlot == null)
        {
            _selectedSlot = slotUI;
            _selectedSlot.SetSelect(true);
        }
        else
        {
            _selectedSlot.SetSelect(false);

            Slot tempSlot = new Slot();
            tempSlot.SlotSetup(slotUI.Slot.Item, slotUI.Slot.ItemsCount);

            if (_selectedSlot.Slot.Item == null)
                _slots[slotUI.transform.GetSiblingIndex()].ClearSlot();
            else
                _slots[slotUI.transform.GetSiblingIndex()].SlotSetup(_selectedSlot.Slot.Item, _selectedSlot.Slot.ItemsCount);

            _inventoryUI.UpdateUISlotInfo(slotUI.transform.GetSiblingIndex());

            if (tempSlot.Item == null)
                _slots[_selectedSlot.transform.GetSiblingIndex()].ClearSlot();
            else
                _slots[_selectedSlot.transform.GetSiblingIndex()].SlotSetup(tempSlot.Item, tempSlot.ItemsCount);

            _inventoryUI.UpdateUISlotInfo(_selectedSlot.transform.GetSiblingIndex());

            _selectedSlot = null;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField, Tooltip("��������� IntentoryUI")] private InventoryUI _inventoryUI;
    
    [Header("Slots")]
    [SerializeField, Tooltip("�����")] private List<Slot> _slots = new();

    public List<Slot> Slots => _slots;

    /// <summary>
    /// �����, ����������� ������� � ���������
    /// </summary>
    /// <param name="item">������ �� ������ ���� Item</param>
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
    /// �����, ����������� ������� � ���������
    /// </summary>
    /// <param name="item">������� ���� Scriptable Item</param>
    /// <param name="count">���������� ����������� ���������</param>
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
    /// ����� ��������� ������� �� ���������
    /// </summary>
    /// <param name="item">������� ���� Scriptable Item</param>
    /// <param name="count">���������� ��������� ���������</param>
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
    /// ����� ����������� ����������� ���������� ��������� � ���������
    /// </summary>
    /// <param name="item">������� ���� Scriptable Item</param>
    /// <param name="count">���������� ����������� ���������</param>
    /// <returns>���������� �������� ���� bool ����������� ����������� ���������� ���������</returns>
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
    /// ����� ����������� ����������� �������� ��������� �� ���������
    /// </summary>
    /// <param name="item">������� ���� Scriptable Item</param>
    /// <param name="count">���������� ��������� ���������</param>
    /// <returns>���������� �������� ���� bool ����������� ����������� �������� ���������</returns>
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
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField, Tooltip("���������")] private Inventory _inventory;

    [Header("UI Slots")]
    [SerializeField, Tooltip("�����")] private List<SlotUI> _slotsUI = new();

    [Header("UI Quick Slots")]
    [SerializeField, Tooltip("����� �������� ��������")] private List<SlotUI> _quickSlotsUI = new();

    public void UpdateUISlotsInfo()
    {
        if (!CheckInventorySlotsCount()) {
            Debug.Log($"Inventory slots count ({_inventory.Slots.Count}) != InventoryUI slots count ({_slotsUI.Count})");
            return;
        }

        for (int i = 0; i < _slotsUI.Count; i++)
        {
            _slotsUI[i].SlotUISetup(_inventory.Slots[i]);
            if (i < _quickSlotsUI.Count)
                _quickSlotsUI[i].SlotUISetup(_inventory.Slots[i]);
        }
    }

    public void UpdateUISlotInfo(int index)
    {
        if (!CheckInventorySlotsCount())
        {
            Debug.Log($"Inventory slots count ({_inventory.Slots.Count}) != InventoryUI slots count ({_slotsUI.Count})");
            return;
        }

        _slotsUI[index].SlotUISetup(_inventory.Slots[index]);
        if (index < _quickSlotsUI.Count)
            _quickSlotsUI[index].SlotUISetup(_inventory.Slots[index]);
    }

    private bool CheckInventorySlotsCount()
    {
        return _inventory.Slots.Count == _slotsUI.Count;
    }
}

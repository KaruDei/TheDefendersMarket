using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private List<SlotUI> _slotsUI;

    private void Start()
    {
        UpdateInventoryUISlotsInfo();
    }

    public void UpdateInventoryUISlotsInfo()
    {
        if (CheckInventorySlotsCount())
        {
            for (int i = 0; i < _slotsUI.Count; i++)
            _slotsUI[i].SetSlot(_inventory.Slots[i]);
        }
        else
        {
            Debug.LogError("Количество слотов Inventory и InventoryUI не равно!");
        }
    }

    private bool CheckInventorySlotsCount()
    {
        return _inventory.Slots.Count == _slotsUI.Count;
    }
}

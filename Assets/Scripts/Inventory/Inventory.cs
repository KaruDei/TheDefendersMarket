using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField, Tooltip("Компонент IntentoryUI")] private InventoryUI _inventoryUI;
    
    [Header("Slots")]
    [SerializeField, Tooltip("Слоты")] private List<Slot> _slots = new();

    private SlotUI _selectedSlot;

    public List<Slot> Slots => _slots;

    /// <summary>
    /// Метод обновления UI слотов при начале игры.
    /// </summary>
    private void Start()
    {
        _inventoryUI.UpdateUISlotsInfo();
    }

    /// <summary>
    /// Метод добавления предмета.
    /// </summary>
    /// <param name="item">Предмет</param>
    public void AddItem(Item item)
    {
        if (item == null) return;

        TryAddToExistingSlots(item);
        TryAddToEmptySlots(item);

        _inventoryUI.UpdateUISlotsInfo();
    }

    /// <summary>
    /// Метод добавления предмета.
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <param name="count">Количество предметов</param>
    public void AddItem(ScriptableItem item, int count = 1)
    {
        if (item == null || count <= 0) return;
        if (!CanAddItem(item, count)) return;

        TryAddToExistingSlots(item, ref count);
        TryAddToEmptySlots(item, ref count);

        _inventoryUI.UpdateUISlotsInfo();
    }

    /// <summary>
    /// Метод удаления предмета.
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <param name="count">Количество предметов</param>
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

    /// <summary>
    /// Метод проверяющий возможность добавления предмета.
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <param name="count">Количество предметов</param>
    /// <returns>Возвращает значение типа bool.</returns>
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

    /// <summary>
    /// Метод проверяющий возможность удаления предмета.
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <param name="count">Количество предметов</param>
    /// <returns>Возвращает занчение типа bool.</returns>
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

    /// <summary>
    /// Метод добавляющий предметы в существующие слоты.
    /// </summary>
    /// <param name="item">Предмет</param>
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

    /// <summary>
    /// Метод добавляющий предметы в существующие слоты.
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <param name="count">Количество предметов</param>
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

    /// <summary>
    /// Метод добавляющий предметы в пустые слоты.
    /// </summary>
    /// <param name="item">Предмет</param>
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

    /// <summary>
    /// Метод добавляющий предметы в пустые слоты.
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <param name="count">Количество предметов</param>
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

    /// <summary>
    /// Метод реализующий логику выбора слота.
    /// Вызов происходит через событие SlotUIGameEvent.
    /// </summary>
    /// <param name="slotUI">Слот</param>
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

            if (slotUI.Slot.Item == _selectedSlot.Slot.Item && slotUI.Slot.GetAvailableQuantity() > 0)
            {
                int amount = Mathf.Min(slotUI.Slot.GetAvailableQuantity(), _selectedSlot.Slot.ItemsCount);
                slotUI.Slot.AddItem(amount);
                _selectedSlot.Slot.RemoveItem(amount);
            }
            else
            {
                Slot tempSlot = new();
                tempSlot.SlotSetup(slotUI.Slot.Item, slotUI.Slot.ItemsCount);

                if (_selectedSlot.Slot.Item == null)
                    _slots[slotUI.transform.GetSiblingIndex()].ClearSlot();
                else
                    _slots[slotUI.transform.GetSiblingIndex()].SlotSetup(_selectedSlot.Slot.Item, _selectedSlot.Slot.ItemsCount);

                if (tempSlot.Item == null)
                    _slots[_selectedSlot.transform.GetSiblingIndex()].ClearSlot();
                else
                    _slots[_selectedSlot.transform.GetSiblingIndex()].SlotSetup(tempSlot.Item, tempSlot.ItemsCount);

            }

            _inventoryUI.UpdateUISlotInfo(slotUI.transform.GetSiblingIndex());
            _inventoryUI.UpdateUISlotInfo(_selectedSlot.transform.GetSiblingIndex());
            _selectedSlot = null;
        }
    }


    public void Save()
    {
        PlayerPrefs.SetString("Inventory", JsonUtility.ToJson(_slots));
        Debug.Log(PlayerPrefs.GetString("Inventory"));
    }

    public bool Load()
    {
        if (PlayerPrefs.HasKey("Inventory"))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("Inventory"), _slots);
            return true;
        }
        else
            return false;
    }
}
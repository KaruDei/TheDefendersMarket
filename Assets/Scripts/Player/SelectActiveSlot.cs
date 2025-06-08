using System.Collections.Generic;
using UnityEngine;

public class SelectActiveSlot : MonoBehaviour
{
    [SerializeField] private List<SlotUI> _quickSlots;

    [Header("Events")]
    [SerializeField] private ScriptableItemGameEvent _onEquipActiveSlotItem;

    private SlotUI _activeSlot;
    private int _index;

    private void Start()
    {
        SelectSlot(0);
    }

    /// <summary>
    /// Метод выбирающий слот.
    /// Вызов происходит через событие StringGameEvent.
    /// </summary>
    /// <param name="key">Название клавиши ввода.</param>
    public void SelectKey(string key)
    {
        switch (key)
        {
            case "1":
                SelectSlot(0);
                break;
            case "2":
                SelectSlot(1);
                break;
            case "3":
                SelectSlot(2);
                break;
            case "4":
                SelectSlot(3);
                break;
        }
    }

    /// <summary>
    /// Метод переключения слота на следующий.
    /// Вызов происходит через событие VoidGameEvent.
    /// </summary>
    public void SelectNextSlot()
    {
        if (_quickSlots.Count == 0 || _index > _quickSlots.Count - 1) return;

        if (_index + 1 >= _quickSlots.Count)
            SelectSlot(0);
        else
            SelectSlot(_index + 1);
    }

    /// <summary>
    /// Метод переключения слота на предыдущий
    /// Вызов происходит через событие VoidGameEvent.
    /// </summary>
    public void SelectPreviousSlot()
    {
        if (_quickSlots.Count == 0 || _index > _quickSlots.Count - 1) return;

        if (_index - 1 < 0)
            SelectSlot(_quickSlots.Count - 1);
        else
            SelectSlot(_index - 1);
    }

    /// <summary>
    /// Метод
    /// </summary>
    private void SelectSlot(int index)
    {
        if (_quickSlots.Count == 0 || index < 0 || index > _quickSlots.Count - 1) return;

        if (_activeSlot != null)
        {
            _activeSlot.SetSelect(false);
            _activeSlot = null;
        }

        _activeSlot = _quickSlots[index];
        _index = index;
        _activeSlot.SetSelect(true);

        if (_activeSlot != null && _activeSlot.Slot != null && _activeSlot.Slot.Item != null)
            _onEquipActiveSlotItem.Raise(_activeSlot.Slot.Item);
        else
            _onEquipActiveSlotItem.Raise(null);
    }
}
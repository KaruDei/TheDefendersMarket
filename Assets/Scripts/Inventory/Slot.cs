using UnityEngine;

[System.Serializable]
public class Slot
{
    [SerializeField] private ScriptableItem _scriptableItem;
    [SerializeField] private int _capacity;

    public bool IsFull;

    public ScriptableItem ScriptableItem => _scriptableItem;
    public int Capacity => _capacity;

    public void SetSlot(ScriptableItem scriptableItem, int count = 1)
    {
        if (scriptableItem == null || count <= 0) return;
        _scriptableItem = scriptableItem;
        _capacity = count;
    }

    public void ClearSlot()
    {
        _scriptableItem = null;
        _capacity = 0;
    }

    public bool AddItem(int count = 1)
    {
        if (count <= 0 || _scriptableItem == null) return false;

        if (_capacity + count <= _scriptableItem.MaxSlotCapacity)
            _capacity += count;
        else
            return false;

        return true;
    }

    public bool RemoveItem(int count = 1)
    {
        if (count <= 0 || _scriptableItem == null) return false;

        if (_capacity - count > 0)
        {
            _capacity -= count;
        }
        else if (_capacity - count == 0)
        {
            ClearSlot();
        }
        else
            return false;

        return true;
    }

    public int GetAvailableQuantity()
    {
        return _scriptableItem.MaxSlotCapacity - _capacity;
    }
}

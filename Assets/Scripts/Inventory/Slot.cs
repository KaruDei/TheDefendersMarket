using UnityEngine;

[System.Serializable]
public class Slot
{
    [Header("Slot Settings")]
    [SerializeField, Tooltip("�������")] private ScriptableItem _item = null;
    [SerializeField, Tooltip("���������� ���������")] private int _itemsCount = 0;

    public ScriptableItem Item => _item;
    public int ItemsCount => _itemsCount;

    /// <summary>
    /// ����� ����������� ����
    /// </summary>
    /// <param name="item">������� ���� Scriptable Item</param>
    /// <param name="itemsCount">���������� ���������</param>
    public void SlotSetup(ScriptableItem item, int itemsCount = 1)
    {
        if (item == null || itemsCount <= 0 || item.MaxSlotCapacity < itemsCount) return;
        _item = item;
        _itemsCount = itemsCount;
    }

    /// <summary>
    /// ����� ��������� ����
    /// </summary>
    public void ClearSlot()
    {
        _item = null;
        _itemsCount = 0;
    }

    /// <summary>
    /// ����� ���������� �������� � ����
    /// </summary>
    /// <param name="count">���������� ����������� ���������</param>
    /// <returns>���������� �������� ���� bool ����������� �� ���������� ����������</returns>
    public bool AddItem(int count = 1)
    {
        if (_item == null || count <= 0) return false;

        if (_itemsCount + count <= _item.MaxSlotCapacity)
            _itemsCount += count;
        else
            return false;

        return true;
    }

    /// <summary>
    /// ����� �������� �������� �� �����
    /// </summary>
    /// <param name="count">���������� ��������� ���������</param>
    /// <returns>���������� �������� ���� bool ����������� �� ���������� ����������</returns>
    public bool RemoveItem(int count = 1)
    {
        if (count <= 0 || _item == null) return false;

        if (_itemsCount - count > 0)
        {
            _itemsCount -= count;
        }
        else if (_itemsCount - count == 0)
        {
            ClearSlot();
        }
        else
            return false;

        return true;
    }

    /// <summary>
    /// ����� ��������� ���������� ���������� ����
    /// </summary>
    /// <returns>���������� ���������� ��������� ����</returns>
    public int GetAvailableQuantity()
    {
        return _item != null ? _item.MaxSlotCapacity - _itemsCount : 0;
    }
}

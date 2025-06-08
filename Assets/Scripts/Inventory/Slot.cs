using UnityEngine;

[System.Serializable]
public class Slot
{
    [Header("Slot Settings")]
    [SerializeField, Tooltip("Предмет")] private ScriptableItem _item = null;
    [SerializeField, Tooltip("Количество предметов")] private int _itemsCount = 0;

    public ScriptableItem Item => _item;
    public int ItemsCount => _itemsCount;

    /// <summary>
    /// Метод заполняющий слот
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <param name="itemsCount">Количество предметов</param>
    public void SlotSetup(ScriptableItem item, int itemsCount = 1)
    {
        if (item == null || itemsCount <= 0 || item.MaxSlotCapacity < itemsCount) return;
        _item = item;
        _itemsCount = itemsCount;
    }

    /// <summary>
    /// Метод очищающий слот
    /// </summary>
    public void ClearSlot()
    {
        _item = null;
        _itemsCount = 0;
    }

    /// <summary>
    /// Метод добавления предмета в слот
    /// </summary>
    /// <param name="count">Количество добавляемых предметов</param>
    /// <returns>Возвращает значение типа bool указываюшее на успешность выполнения</returns>
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
    /// Метод удаления предмета из слота
    /// </summary>
    /// <param name="count">Количество удаляемых предметов</param>
    /// <returns>Возвращает значение типа bool указываюшее на успешность выполнения</returns>
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
    /// Метод получения доступного количества мест
    /// </summary>
    /// <returns>Возвращает количество доступных мест</returns>
    public int GetAvailableQuantity()
    {
        return _item != null ? _item.MaxSlotCapacity - _itemsCount : 0;
    }
}

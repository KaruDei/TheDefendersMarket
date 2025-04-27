using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField, Tooltip("Слоты")] private List<Slot> _slots = new();

    [Header("Inventory UI")]
    [SerializeField, Tooltip("Компонент IntentoryUI")] private InventoryUI _inventoryUI;

    public List<Slot> Slots => _slots;

    /// <summary>
    /// Метод, добавляющий предмет в инвентарь
    /// </summary>
    /// <param name="item">Ссылка на объект типа Item</param>
    public void AddItem(Item item)
    {
        
    }

    /// <summary>
    /// Метод, добавляющий предмет в инвентарь
    /// </summary>
    /// <param name="item">Предмет типа Scriptable Item</param>
    /// <param name="count">Количество добавляемых предметов</param>
    public void AddItem(ScriptableItem item, int count = 1)
    {

    }

    /// <summary>
    /// Метод удаляющий предмет из инвентаря
    /// </summary>
    /// <param name="item">Ссылка на объект типа Item</param>
    public void RemoveItem(Item item)
    {

    }

    /// <summary>
    /// Метод удаляющий предмет из инвентаря
    /// </summary>
    /// <param name="item">Предмет типа Scriptable Item</param>
    /// <param name="count">Количество удаляемых предметов</param>
    public void RemoveItem(ScriptableItem item, int count = 1)
    {

    }
}

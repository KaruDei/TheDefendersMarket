using TMPro;
using UnityEngine;

public class ItemInfoField : MonoBehaviour
{
    [SerializeField, Tooltip("Поле названия")] private TextMeshProUGUI _itemName;
    [SerializeField, Tooltip("Поле описания")] private TextMeshProUGUI _itemDescription;

    /// <summary>
    /// Отчистка текста при старте.
    /// </summary>
    private void Start()
    {
        _itemName.text = string.Empty;
        _itemDescription.text = string.Empty;
    }

    /// <summary>
    /// Метод, изменяющий информацию о выбранном предмете.
    /// </summary>
    /// <param name="itemName">Название предмета</param>
    /// <param name="itemDescription">Описание предмета</param>
    public void SetItemInfo(string itemName, string itemDescription)
    {
        _itemName.text = itemName;
        _itemDescription.text = itemDescription;
    }

    /// <summary>
    /// Метод, изменяющий информацию о выбранном предмете.
    /// Вызов происходит через событие SlotGameEvent.
    /// </summary>
    /// <param name="slot">Слот</param>
    public void SetItemInfo(Slot slot)
    {
        if (slot == null || slot.Item == null)
        {
            _itemName.text = string.Empty;
            _itemDescription.text = string.Empty;
        }
        else
        {
            _itemName.text = slot.Item.Name;
            _itemDescription.text = slot.Item.Description;
        }
    }
}

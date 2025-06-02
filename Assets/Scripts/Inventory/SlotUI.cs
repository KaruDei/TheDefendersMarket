using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, ISelectHandler
{
    [Header("Slot UI Components")]
    [SerializeField, Tooltip("Компонент изображения слота")] private Image _imageSlot;
    [SerializeField, Tooltip("Компонент изображения предмета")] private Image _imageItem;
    [SerializeField, Tooltip("Текст количества предметов")] private TextMeshProUGUI _textItemCount;

    [Header("Slot UI Sprites")]
    [SerializeField, Tooltip("Изображение слота")] private Sprite _slotBaseSprite;
    [SerializeField, Tooltip("Изображение выделенного слота")] private Sprite _slotSelectedSprite;

    [Header("Events")]
    [SerializeField, Tooltip("Событие обновления поля информации о предмете")] private SlotGameEvent _onSlotInfo;

    [Header("Slot")]
    [Tooltip("Ссылка на слот")] public Slot Slot;

    private Color _baseColor = Color.white;
    private Color _emptyColor = Color.clear;

    /// <summary>
    /// Метод заполняющий слот.
    /// </summary>
    /// <param name="slot">Ссылка на Slot с которого будет браться информация.</param>
    public void SlotUISetup(Slot slot)
    {
        if (slot == null) return;

        Slot = slot;
        
        if (Slot.Item != null)
        {
            _imageItem.sprite = Slot.Item.Sprite;
            _imageItem.color = _baseColor;

            if (Slot.ItemsCount == 1) 
                _textItemCount.text = string.Empty;
            else 
                _textItemCount.text = Slot.ItemsCount.ToString();
        }
        else
        {
            _imageItem.sprite = null;
            _imageItem.color = _emptyColor;
            _textItemCount.text = string.Empty;
        }
    }

    /// <summary>
    /// Метод, меняющий вид слота в зависимости от выделения.
    /// </summary>
    /// <param name="state">Принимает bool значение указывающее на выделение.</param>
    public void SetSelect(bool state)
    {
        if (state)
            _imageSlot.sprite = _slotSelectedSprite;
        else
            _imageSlot.sprite = _slotBaseSprite;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSlotInfo.Raise(Slot);
    }
}

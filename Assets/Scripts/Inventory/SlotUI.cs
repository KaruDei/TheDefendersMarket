using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, ISelectHandler
{
    [Header("Slot UI Components")]
    [SerializeField, Tooltip("��������� ����������� �����")] private Image _imageSlot;
    [SerializeField, Tooltip("��������� ����������� ��������")] private Image _imageItem;
    [SerializeField, Tooltip("����� ���������� ���������")] private TextMeshProUGUI _textItemCount;

    [Header("Slot UI Sprites")]
    [SerializeField, Tooltip("����������� �����")] private Sprite _slotBaseSprite;
    [SerializeField, Tooltip("����������� ����������� �����")] private Sprite _slotSelectedSprite;

    [Header("Events")]
    [SerializeField, Tooltip("������� ���������� ���� ���������� � ��������")] private SlotGameEvent _onSlotInfo;

    [Header("Slot")]
    [Tooltip("������ �� ����")] public Slot Slot;

    private Color _baseColor = Color.white;
    private Color _emptyColor = Color.clear;

    /// <summary>
    /// ����� ����������� ����.
    /// </summary>
    /// <param name="slot">������ �� Slot � �������� ����� ������� ����������.</param>
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
    /// �����, �������� ��� ����� � ����������� �� ���������.
    /// </summary>
    /// <param name="state">��������� bool �������� ����������� �� ���������.</param>
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

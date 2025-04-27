using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [Header("Slot UI Components")]
    [SerializeField, Tooltip("��������� �����������")] private Image _image;
    [SerializeField, Tooltip("����� ���������� ���������")] private TextMeshProUGUI _textItemCount;

    [Header("Slot")]
    [SerializeField, Tooltip("����")] private Slot _slot;

    private Color _baseColor = Color.white;
    private Color _emptyColor = Color.clear;

    /// <summary>
    /// ����� ����������� ����
    /// </summary>
    /// <param name="slot">������ �� Slot � �������� ����� ������� ����������</param>
    public void SlotUISetup(Slot slot)
    {
        if (slot == null) return;

        _slot = slot;
        
        if (_slot.Item != null)
        {
            _image.sprite = _slot.Item.Sprite;
            _image.color = _baseColor;

            if (_slot.ItemsCount == 1) 
                _textItemCount.text = string.Empty;
            else 
                _textItemCount.text = _slot.ItemsCount.ToString();
        }
        else
        {
            _image.color = _emptyColor;
            _textItemCount.text = string.Empty;
        }
    }
}

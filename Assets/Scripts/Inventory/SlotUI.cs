using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{

    [Header("Slot UI Components")]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _countText;

    private Slot _slot;
    
    private Color baseColor;
    private Color emptyColor;

    private void Start()
    {
        baseColor = Color.white;
        emptyColor = Color.clear;
    }

    public void SetSlot(Slot slot)
    {
        if (slot == null) return;

        _slot = slot;

        if (_slot.ScriptableItem != null)
        {
            _image.sprite = _slot.ScriptableItem.Sprite;
            _image.color = baseColor;
            _countText.text = $"{_slot.Capacity}";
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        _image.sprite = null;
        _image.color = emptyColor;
        _countText.text = string.Empty;
    }
}

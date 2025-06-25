using TMPro;
using UnityEngine;

public class PriceInputFiled : MonoBehaviour
{
    [SerializeField] private TMP_InputField _InputField;

    [SerializeField] private MarketSlot _slot;

    [SerializeField] private VoidGameEvent _onPlayerGame;

    public void Setup(MarketSlot slot)
    {
        if (slot == null) return;
        
        _slot = slot;

        if (slot.Item != null)
        {
            _InputField.text = slot.CurrentPrice.ToString();
        }
    }

    public void ApplyPrice()
    {
        if (_slot != null)
        {
            Debug.Log(_InputField.text);
            if (int.TryParse(_InputField.text, out int value))
                _slot.SetPrice(value);

            _slot = null;
        }

        _onPlayerGame.Raise();
    }
}

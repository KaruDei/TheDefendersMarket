using TMPro;
using UnityEngine;

public class MarketPriceTable : MonoBehaviour, IInteractable
{
    [SerializeField] private MarketSlot _slot;
    [SerializeField, Tooltip("Компонент текста")] private TextMeshProUGUI _text;

    [Header("Events")]
    [SerializeField] private VoidGameEvent _onPlayerPriceField;
    [SerializeField] private MarketSlotGameEvent _onSetPriceToField;

    public void Interact()
    {
        Debug.Log(gameObject.name + " Interact");
        _onPlayerPriceField.Raise();
        _onSetPriceToField.Raise(_slot);
    }

    /// <summary>
    /// Метод отображения установленной цены на предмет.
    /// </summary>
    /// <param name="text"></param>
    public void Setup(string text)
    {
        _text.text = text;
    }
}

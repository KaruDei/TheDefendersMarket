using TMPro;
using UnityEngine;

public class MarketPriceTable : MonoBehaviour
{
    [SerializeField, Tooltip("Компонент текста")] private TextMeshProUGUI _text;

    /// <summary>
    /// Метод отображения установленной цены на предмет.
    /// </summary>
    /// <param name="text"></param>
    public void Setup(string text)
    {
        _text.text = text;
    }
}

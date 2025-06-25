using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderDeskUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Button _button;

    [SerializeField] private Order _order;

    private void Start()
    {
        UpdateInfoPanel(null);
    }

    public void UpdateInfoPanel(Order order)
    {
        _order = order;
        if (order == null)
        {
            _title.text = string.Empty;
            _description.text = string.Empty;
            _button.gameObject.SetActive(false);
        }
        else
        {
            _title.text = order.Data.Title;
            _description.text = order.Data.Description;
            _button.gameObject.SetActive(true);
        }
    }

    public void StartOrderQuest()
    {
        if (_order == null) return;

        _order.StartOrderQuest();
    }
}

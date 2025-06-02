using TMPro;
using UnityEngine;

public class ItemInfoField : MonoBehaviour
{
    [SerializeField, Tooltip("���� ��������")] private TextMeshProUGUI _itemName;
    [SerializeField, Tooltip("���� ��������")] private TextMeshProUGUI _itemDescription;

    /// <summary>
    /// �������� ������ ��� ������.
    /// </summary>
    private void Start()
    {
        _itemName.text = string.Empty;
        _itemDescription.text = string.Empty;
    }

    /// <summary>
    /// �����, ���������� ���������� � ��������� ��������.
    /// </summary>
    /// <param name="itemName">�������� ��������.</param>
    /// <param name="itemDescription">�������� ��������.</param>
    public void SetItemInfo(string itemName, string itemDescription)
    {
        _itemName.text = itemName;
        _itemDescription.text = itemDescription;
    }

    /// <summary>
    /// �����, ���������� ���������� � ��������� ��������.
    /// ����� ���������� ����� ������� SlotGameEvent.
    /// </summary>
    /// <param name="slot">��������� ������ ���� Slot.</param>
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

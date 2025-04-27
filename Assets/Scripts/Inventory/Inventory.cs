using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField, Tooltip("�����")] private List<Slot> _slots = new();

    [Header("Inventory UI")]
    [SerializeField, Tooltip("��������� IntentoryUI")] private InventoryUI _inventoryUI;

    public List<Slot> Slots => _slots;

    /// <summary>
    /// �����, ����������� ������� � ���������
    /// </summary>
    /// <param name="item">������ �� ������ ���� Item</param>
    public void AddItem(Item item)
    {
        
    }

    /// <summary>
    /// �����, ����������� ������� � ���������
    /// </summary>
    /// <param name="item">������� ���� Scriptable Item</param>
    /// <param name="count">���������� ����������� ���������</param>
    public void AddItem(ScriptableItem item, int count = 1)
    {

    }

    /// <summary>
    /// ����� ��������� ������� �� ���������
    /// </summary>
    /// <param name="item">������ �� ������ ���� Item</param>
    public void RemoveItem(Item item)
    {

    }

    /// <summary>
    /// ����� ��������� ������� �� ���������
    /// </summary>
    /// <param name="item">������� ���� Scriptable Item</param>
    /// <param name="count">���������� ��������� ���������</param>
    public void RemoveItem(ScriptableItem item, int count = 1)
    {

    }
}

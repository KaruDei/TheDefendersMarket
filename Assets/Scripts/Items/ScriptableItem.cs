using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item/Item")]
public class ScriptableItem : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField, Tooltip("�������� ��������")] private string _name;
    [SerializeField, TextArea, Tooltip("�������� ��������")] private string _description;
    
    [Header("Item Rank")]
    [SerializeField, Tooltip("���� ��������")] private Rank _itemRank;

    [Header("Item Slot Settings")]
    [SerializeField, Tooltip("������������ ���������� ��������� � �����")] private int _maxStackCapacity;

    [Header("Item Drop Settings")]
    [SerializeField, Range(1, 10), Tooltip("������������ ���������� ���������� ���������")] private int _maxDropCount;

    [Header("Item Price")]
    [SerializeField, Tooltip("����")] private int _price;

    [Header("Item Sprite")]
    [SerializeField, Tooltip("�����������")] private Sprite _sprite;

    [Header("Item Object")]
    [SerializeField, Tooltip("������� ������")] private GameObject _prefab;

    public string Name => _name;
    public string Description => _description;
    public int MaxSlotCapacity => _maxStackCapacity;
    public int MaxDropCount => _maxDropCount;
    public int Price => _price;
    public Sprite Sprite => _sprite;
    public GameObject Prefab => _prefab;
}

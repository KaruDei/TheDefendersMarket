using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item/Item")]
public class ScriptableItem : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField, Tooltip("Название предмета")] private string _name;
    [SerializeField, TextArea, Tooltip("Описание предмета")] private string _description;
    
    [Header("Item Rank")]
    [SerializeField, Tooltip("Ранг предмета")] private Rank _itemRank;

    [Header("Item Slot Settings")]
    [SerializeField, Tooltip("Максимальное количество предметов в стаке")] private int _maxStackCapacity;

    [Header("Item Drop Settings")]
    [SerializeField, Range(1, 10), Tooltip("Максимальное количество выпадаемых предметов")] private int _maxDropCount;

    [Header("Item Price")]
    [SerializeField, Tooltip("Цена")] private int _price;

    [Header("Item Sprite")]
    [SerializeField, Tooltip("Изображение")] private Sprite _sprite;

    [Header("Item Object")]
    [SerializeField, Tooltip("Игровой объект")] private GameObject _prefab;

    public string Name => _name;
    public string Description => _description;
    public int MaxSlotCapacity => _maxStackCapacity;
    public int MaxDropCount => _maxDropCount;
    public int Price => _price;
    public Sprite Sprite => _sprite;
    public GameObject Prefab => _prefab;
}

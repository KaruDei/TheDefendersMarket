using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Items", order = 0)]
public class ScriptableItem : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [Header("Item Slot Settings")]
    [SerializeField] private int _maxSlotCapacity;

    [Header("Item Drop Settings")]
    [SerializeField, Range(1, 10)] private int _maxDropCount;

    [Header("Item Price")]
    [SerializeField] private float _price;

    [Header("Item Sprite")]
    [SerializeField] private Sprite _sprite;

    [Header("Item Object")]
    [SerializeField] private GameObject _prefab;

    public string Name => _name;
    public string Description => _description;
    public int MaxSlotCapacity => _maxSlotCapacity;
    public int MaxDropCount => _maxDropCount;
    public float Price => _price;
    public Sprite Sprite => _sprite;
    public GameObject Prefab => _prefab;
}

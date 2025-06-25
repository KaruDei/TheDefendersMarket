using UnityEngine;

public class MarketSlot : MonoBehaviour, IInteractable
{
    [SerializeField] private MarketPriceTable _marketPriceTable;

    [Header("Events")]
    [SerializeField] private MarketSlotGameEvent _onPutItem;
    [SerializeField] private VoidGameEvent _onItemUsed;

    [SerializeField] private Item _item;
    public Item Item => _item;

    [SerializeField] private int _currentPrice;
    public int CurrentPrice => _currentPrice;

    private void FixedUpdate()
    {
        if (_item == null && _currentPrice != 0) SetPrice(0);
    }

    public void Interact()
    {
        Debug.Log(gameObject.name + " Interact");
        if (_item == null)
        {
            _onPutItem?.Raise(this);
        }
    }

    public void Setup(Item item)
    {
        _item = Instantiate(item.ScriptableItem.Prefab, transform.position, transform.rotation, transform).TryGetComponent(out Item i) ? i : null;
        _onItemUsed?.Raise();
        SetPrice(_item != null ? _item.ScriptableItem.Price : 0);
    }

    public void Setup(ScriptableItem item)
    {
        _item = Instantiate(item.Prefab, transform.position, transform.rotation, transform).TryGetComponent(out Item i) ? i : null;
        _onItemUsed?.Raise();
        SetPrice(_item != null ? _item.ScriptableItem.Price : 0);
    }

    public void SetPrice(int price = 1)
    {
        if (_item != null)
        {
            _currentPrice = price;
            _marketPriceTable.Setup(_currentPrice.ToString());
        }
        else
        {
            _currentPrice = 0;
            _marketPriceTable.Setup(_currentPrice.ToString());
        }
    }
}

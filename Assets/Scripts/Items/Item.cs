using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IUseable
{
    [Header("Scriptable Item")]
    [SerializeField] private ScriptableItem _scriptableItem;

    [Header("Events")]
    [SerializeField] private ItemGameEvent _onItemUsed;
    [SerializeField] private ItemGameEvent _onItemPickup;

    [Header("Item Count")]
    public int ItemCount;
    
    public ScriptableItem ScriptableItem => _scriptableItem;

    public void RemoveItem(int count = 1)
    {
        if (count > ItemCount || count < 0) return;

        ItemCount -= count;
        if (ItemCount == 0)
            Destroy(gameObject);
    }

    public void Interact()
    {
        _onItemPickup.Raise(this);
    }

    public void Use()
    {
        _onItemUsed.Raise(this);
    }
}

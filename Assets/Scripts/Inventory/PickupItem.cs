using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private Item _item;

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.TryGetComponent(out Inventory inventory))
    //    {
    //        int count = inventory.AddItem(_item.ScriptableItem, _item.ItemCount);

    //        if (count == 0)
    //            _item.RemoveItem(_item.ItemCount);
    //        else if (count > 0)
    //            _item.RemoveItem(_item.ItemCount - count);
    //        else
    //            Debug.LogError("Count < 0");
    //    }
    //}
}

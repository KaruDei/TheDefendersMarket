using System.Collections.Generic;
using UnityEngine;

public class EquipActiveSlotItem : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private Dictionary<string, GameObject> _poolDictionary = new();

    private GameObject _currentActive;

    /// <summary>
    /// Метод экипировки выбранного предмета.
    /// Вызывается через событие ScriptableItemGameEvent.
    /// </summary>
    /// <param name="item">Предмет</param>
    public void EquipItem(ScriptableItem item)
    {
        if (item == null)
        {
            _currentActive?.SetActive(false);
            _currentActive = null;
            return;
        }

        _currentActive?.SetActive(false);

        if (_poolDictionary.TryGetValue(item.name, out GameObject itemObject))
        {
            itemObject.SetActive(true);
            _currentActive = itemObject;
        }
        else
        {
            GameObject obj = Instantiate(item.Prefab, _spawnPoint);
            
            if (obj.TryGetComponent(out Rigidbody objRb))
                objRb.isKinematic = true;
            if (obj.TryGetComponent(out Collider objCol))
                objCol.isTrigger = true;

            _poolDictionary.Add(item.name, obj);
            _currentActive = obj;
        }
    }

    public void UseSelectItem()
    {
        if (_currentActive != null && _currentActive.TryGetComponent(out Item item))
        {
            item.Use();
        }
    }

    public void PutSelectItem(MarketSlot slot)
    {
        if (_currentActive != null && _currentActive.TryGetComponent(out Item item))
        {
            slot.Setup(item);
        }
    }
}

using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ScriptableItem _scriptableItem;

    public ScriptableItem ScriptableItem => _scriptableItem;

    public int ItemCount;

    private void Start()
    {
        ItemCount = Random.Range(1, _scriptableItem.MaxDropCount);
    }

    public void RemoveItem(int count = 1)
    {
        if (count > ItemCount || count < 0) return;
        
        ItemCount -= count;
        if (ItemCount == 0) 
            Destroy(gameObject);
    }
}

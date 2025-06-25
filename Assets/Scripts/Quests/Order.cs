using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Order : MonoBehaviour, ISelectHandler
{
    [SerializeField] private OrderData _data;
    public OrderData Data => _data;

    [SerializeField] private TextMeshProUGUI _title;

    [SerializeField] private OrderGameEvent _onOrderInfo;
    [SerializeField] private IntGameEvent _onLoadScene;
    [SerializeField] private VoidGameEvent _onSave;

    private void Start()
    {
        _title.text = _data.Title;
    }

    public void StartOrderQuest()
    {
        // сохранение
        _onSave.Raise();
        _onLoadScene.Raise(_data.SceneIndex);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onOrderInfo.Raise(this);
    }

    public void Save()
    {

    }

    public void Load()
    {

    }
}

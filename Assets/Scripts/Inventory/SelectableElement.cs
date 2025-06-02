using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableElement : MonoBehaviour, IPointerEnterHandler
{
    private EventSystem _eventSystem;

    /// <summary>
    /// ћетод, кэширующий текущую систему событий.
    /// </summary>
    private void Start()
    {
        _eventSystem = EventSystem.current;
    }

    /// <summary>
    /// ћетод, выдел€ющий слот
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _eventSystem.SetSelectedGameObject(gameObject);
    }
}

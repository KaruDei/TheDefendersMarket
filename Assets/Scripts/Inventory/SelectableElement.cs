using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableElement : MonoBehaviour, IPointerEnterHandler
{
    private EventSystem _eventSystem;

    /// <summary>
    /// Метод, кэширующий текущую систему событий.
    /// </summary>
    private void Start()
    {
        _eventSystem = EventSystem.current;
    }

    /// <summary>
    /// Метод, выделяющий элемент
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _eventSystem.SetSelectedGameObject(gameObject);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableElement : MonoBehaviour, IPointerEnterHandler
{
    private EventSystem _eventSystem;

    /// <summary>
    /// �����, ���������� ������� ������� �������.
    /// </summary>
    private void Start()
    {
        _eventSystem = EventSystem.current;
    }

    /// <summary>
    /// �����, ���������� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _eventSystem.SetSelectedGameObject(gameObject);
    }
}

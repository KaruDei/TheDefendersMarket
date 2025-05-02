using UnityEngine;
using UnityEngine.EventSystems;

public class UIWindowSelector : MonoBehaviour
{
    [SerializeField, Tooltip("Выбираемый объект")] private GameObject _selectableObject;

    private void OnEnable()
    {
        SelectObject();
    }

    /// <summary>
    /// Выюирает объект для работы навигации New Input System
    /// </summary>
    private void SelectObject()
    {
        if (_selectableObject != null)
            EventSystem.current.SetSelectedGameObject(_selectableObject);
        else
            Debug.LogWarning("Объект не выбран!");
    }
}

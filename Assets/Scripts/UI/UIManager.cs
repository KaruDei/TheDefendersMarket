using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Header("UI Windows")]
    [SerializeField, Tooltip("Окно игровых индикаторво")] private GameObject _gameIndicators;
    [SerializeField, Tooltip("Окно инвентаря")] private GameObject _inventory;
    [SerializeField, Tooltip("Окно меню")] private GameObject _pauseMenu;
    [SerializeField, Tooltip("Окно настроек")] private GameObject _options;

    [Header("Events")]
    [SerializeField, Tooltip("Событие смены карты")] private StringGameEvent _onSwitchMap;

    private string _currentDevice;

    public UIStates State { get; private set; }

    private void Start()
    {
        Game();
    }

    public void UpdateUIWindow()
    {
        switch (State)
        {
            case UIStates.GAME:
                _gameIndicators.SetActive(true);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _options.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                HideCursor();
                _onSwitchMap.Raise("Player");
                break;

            case UIStates.OPTIONS:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _options.SetActive(true);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;

            case UIStates.INVENTORY:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(true);
                _pauseMenu.SetActive(false);
                _options.SetActive(false);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;

            case UIStates.PAUSE_MENU:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(true);
                _options.SetActive(false);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;
        }
    }

    public void SetState(UIStates state)
    {
        State = state;
        UpdateUIWindow();
    }

    /// <summary>
    /// Метод, изменяющий название устройства ввода.
    /// Вызывается через событие StringGameEvent.
    /// </summary>
    /// <param name="deviceName">Название устройства ввода.</param>
    public void SetCurrentDevice(string deviceName)
    {
        _currentDevice = deviceName;
        UpdateUIWindow();
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void VisableCursor()
    {
        if (_currentDevice == "Gamepad")
        {
            HideCursor();
            return;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Game()
    {
        Debug.Log("Game");
        SetState(UIStates.GAME);
    }

    public void Inventory()
    {
        Debug.Log("Inventory");
        if (State == UIStates.INVENTORY)
            SetState(UIStates.GAME);
        else
            SetState(UIStates.INVENTORY);
    }
    
    public void PauseMenu()
    {
        Debug.Log("Pause");
        if (State == UIStates.PAUSE_MENU)
            SetState(UIStates.GAME);
        else
            SetState(UIStates.PAUSE_MENU);
    }

    public void Options()
    {
        Debug.Log("Options");
        if (State == UIStates.OPTIONS)
            SetState(UIStates.PAUSE_MENU);
        else
            SetState(UIStates.OPTIONS);
    }
}

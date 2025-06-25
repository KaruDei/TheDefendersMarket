using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Header("UI Windows")]
    [SerializeField, Tooltip("Окно игровых индикаторво")] private GameObject _gameIndicators;
    [SerializeField, Tooltip("Окно инвентаря")] private GameObject _inventory;
    [SerializeField, Tooltip("Окно меню")] private GameObject _pauseMenu;
    [SerializeField, Tooltip("Окно умений")] private GameObject _skills;
    [SerializeField, Tooltip("Окно умений")] private GameObject _quests;
    [SerializeField, Tooltip("Окно умений")] private GameObject _priceInput;
    [SerializeField, Tooltip("Окно умений")] private GameObject _orders;
    [SerializeField, Tooltip("Окно умений")] private GameObject _loadScreen;

    [Header("State")]
    [SerializeField] private UIStates _startState;

    [Header("Events")]
    [SerializeField, Tooltip("Событие смены карты")] private StringGameEvent _onSwitchMap;

    private string _currentDevice;

    public UIStates State { get; private set; }

    private void Start()
    {
        SetState(_startState);
    }

    public void UpdateUIWindow()
    {
        switch (State)
        {
            case UIStates.GAME:
                _gameIndicators.SetActive(true);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _skills.SetActive(false);
                _quests.SetActive(false);
                _priceInput.SetActive(false);
                _orders.SetActive(false);
                _loadScreen.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                HideCursor();
                _onSwitchMap.Raise("Player");
                break;

            case UIStates.SKILLS:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _skills.SetActive(true);
                _quests.SetActive(false);
                _priceInput.SetActive(false);
                _orders.SetActive(false);
                _loadScreen.SetActive(false);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;

            case UIStates.INVENTORY:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(true);
                _pauseMenu.SetActive(false);
                _skills.SetActive(false);
                _quests.SetActive(false);
                _priceInput.SetActive(false);
                _orders.SetActive(false);
                _loadScreen.SetActive(false);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;

            case UIStates.PAUSE_MENU:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(true);
                _skills.SetActive(false);
                _quests.SetActive(false);
                _priceInput.SetActive(false);
                _orders.SetActive(false);
                _loadScreen.SetActive(false);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;

            case UIStates.QUESTS:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _skills.SetActive(false);
                _quests.SetActive(true);
                _priceInput.SetActive(false);
                _orders.SetActive(false);
                _loadScreen.SetActive(false);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;

            case UIStates.PRICE_INPUT:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _skills.SetActive(false);
                _quests.SetActive(false);
                _priceInput.SetActive(true);
                _orders.SetActive(false);
                _loadScreen.SetActive(false);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;
            
            case UIStates.ORDERS:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _skills.SetActive(false);
                _quests.SetActive(false);
                _priceInput.SetActive(false);
                _orders.SetActive(true);
                _loadScreen.SetActive(false);
                _onSwitchMap.Raise("UI");
                VisableCursor();
                break;

            case UIStates.LOAD_SCREEN:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _skills.SetActive(false);
                _quests.SetActive(false);
                _priceInput.SetActive(false);
                _orders.SetActive(false);
                _loadScreen.SetActive(true);
                _onSwitchMap.Raise("UI");
                HideCursor();
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
        //Debug.Log("Game");
        SetState(UIStates.GAME);
    }

    public void Quests()
    {
        //Debug.Log("Quests");
        SetState(UIStates.QUESTS);
    }
    
    public void LoadScreen()
    {
        //Debug.Log("LoadScreen");
        SetState(UIStates.LOAD_SCREEN);
    }

    public void Orders()
    {
        //Debug.Log("Orders");
        SetState(UIStates.ORDERS);
    }

    public void PriceInput()
    {
        //Debug.Log("PriceInput");
        SetState(UIStates.PRICE_INPUT);
    }

    public void Inventory()
    {
        //Debug.Log("Inventory");
        //if (State == UIStates.INVENTORY)
        //    SetState(UIStates.GAME);
        //else
            SetState(UIStates.INVENTORY);
    }
    
    public void PauseMenu()
    {
        //Debug.Log("Pause");
        //if (State == UIStates.PAUSE_MENU)
        //    SetState(UIStates.GAME);
        //else
            SetState(UIStates.PAUSE_MENU);
    }

    public void Skills()
    {
        //Debug.Log("Skills");
        //if (State == UIStates.SKILLS)
        //    SetState(UIStates.PAUSE_MENU);
        //else
            SetState(UIStates.SKILLS);
    }
}

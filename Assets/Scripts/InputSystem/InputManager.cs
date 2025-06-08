using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField, Tooltip("Компонент PlayerInput")] private PlayerInput _playerInput;

    [Header("Maps")]
    [SerializeField, Tooltip("Название карты действий игрока")] private string _playerMapName = "Player";
    [SerializeField, Tooltip("Название карты действий UI")] private string _uiMapName = "UI";

    [Header("Events")]
    [SerializeField, Tooltip("Событие смены текущего устройства ввода")] private StringGameEvent _switchCurrentDevice;
    //[SerializeField, Tooltip("Событие смены карты управления")] private StringGameEvent _switchMapGameEvent;
    [SerializeField, Tooltip("Событие атаки")] private VoidGameEvent _attackGameEvent;
    [SerializeField, Tooltip("Событие прицеливания")] private BoolGameEvent _aimGameEvent;
    [SerializeField, Tooltip("Событие взаимодействия")] private VoidGameEvent _interactGameEvent;
    [SerializeField, Tooltip("Событие прыжка")] private VoidGameEvent _jumpGameEvent;
    [SerializeField, Tooltip("Событие паузы")] private VoidGameEvent _pauseGameEvent;
    [SerializeField, Tooltip("Событие открытия инвентаря")] private VoidGameEvent _inventoryGameEvent;
    [SerializeField, Tooltip("Событие выбора предыдущего элемента")] private VoidGameEvent _previousGameEvent;
    [SerializeField, Tooltip("Событие выбора следующего элемента")] private VoidGameEvent _nextGameEvent;
    [SerializeField, Tooltip("Событие выбора слотов (1-4)")] private StringGameEvent _selectSlot;

    private InputActionMap _playerMap;
    private InputActionMap _uiMap;
    private string _currentMapName;

    private InputAction _attackAction;
    private InputAction _aimAction;
    private InputAction _interactAction;
    private InputAction _jumpAction;
    private InputAction _pauseAction;
    private InputAction _inventoryAction;
    private InputAction _previousAction;
    private InputAction _nextAction;
    private InputAction _selectSlotAction;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _sprintAction;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Sprint { get; private set; }

    /// <summary>
    /// Кэширование полей
    /// </summary>
    private void Awake()
    {
        if (_playerInput == null) return;

        _playerMap = _playerInput.actions.FindActionMap(_playerMapName);
        _uiMap = _playerInput.actions.FindActionMap(_uiMapName);

        _moveAction = _playerMap.FindAction("Move");
        _lookAction = _playerMap.FindAction("Look");
        _sprintAction = _playerMap.FindAction("Sprint");

        _attackAction = _playerMap.FindAction("Attack");
        _aimAction = _playerMap.FindAction("Aim");
        _interactAction = _playerMap.FindAction("Interact");
        _jumpAction = _playerMap.FindAction("Jump");
        _pauseAction = _playerMap.FindAction("Pause");
        _inventoryAction = _playerMap.FindAction("Inventory");
        _nextAction = _playerMap.FindAction("Next");
        _previousAction = _playerMap.FindAction("Previous");
        _selectSlotAction = _playerMap.FindAction("SelectSlot");
    }

    private void OnEnable()
    {
        _playerInput.onControlsChanged += ControlChange;

        _currentMapName = string.IsNullOrEmpty(_currentMapName) ? _playerMapName : _currentMapName;
        SwitchActionMap(_currentMapName);
        BindActions();
    }

    private void OnDisable()
    {
        _playerInput.onControlsChanged -= ControlChange;

        _currentMapName = _playerInput.currentActionMap.name;
        UnbindActions();
    }

    /// <summary>
    /// Метод, сообщающий, что изменилось утройство ввода.
    /// </summary>
    /// <param name="playerInput">Принимает значение типа PlayerInput</param>
    private void ControlChange(PlayerInput playerInput)
    {
        if (playerInput.currentControlScheme == "Gamepad")
            _switchCurrentDevice.Raise("Gamepad");
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
            _switchCurrentDevice.Raise("Keyboard&Mouse");
    }

    /// <summary>
    /// Метод преключения карты действий.
    /// </summary>
    /// <param name="mapName">Принимает название карты действий</param>
    public void SwitchActionMap(string mapName)
    {
        if (mapName != _playerMapName && mapName != _uiMapName)
        {
            Debug.LogWarning("Нет карты действий с таким названием!");
            return;
        }

        _playerInput.SwitchCurrentActionMap(mapName);
    }

    private void BindActions()
    {
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;

        _lookAction.performed += OnLook;
        _lookAction.canceled += OnLook;

        _sprintAction.performed += OnSprint;
        _sprintAction.canceled += OnSprint;

        _attackAction.performed += _ => _attackGameEvent.Raise();

        _aimAction.performed += _ => _aimGameEvent.Raise(true);
        _aimAction.canceled += _ => _aimGameEvent.Raise(false);

        _interactAction.performed += _ => _interactGameEvent.Raise();

        _jumpAction.performed += _ => _jumpGameEvent.Raise();

        _pauseAction.performed += _ => _pauseGameEvent.Raise();

        _inventoryAction.performed += _ => _inventoryGameEvent.Raise();

        _nextAction.performed += _ => _nextGameEvent.Raise();

        _previousAction.performed += _ => _previousGameEvent.Raise();

        _selectSlotAction.performed += ctx => _selectSlot.Raise(ctx.control.name);
    }

    private void UnbindActions()
    {
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;

        _lookAction.performed -= OnLook;
        _lookAction.canceled -= OnLook;

        _sprintAction.performed -= OnSprint;
        _sprintAction.canceled -= OnSprint;

        _attackAction.performed -= _ => _attackGameEvent.Raise();

        _aimAction.performed -= _ => _aimGameEvent.Raise(true);
        _aimAction.canceled -= _ => _aimGameEvent.Raise(false);

        _interactAction.performed -= _ => _interactGameEvent.Raise();

        _jumpAction.performed -= _ => _jumpGameEvent.Raise();

        _pauseAction.performed -= _ => _pauseGameEvent.Raise();

        _inventoryAction.performed -= _ => _inventoryGameEvent.Raise();

        _nextAction.performed -= _ => _nextGameEvent.Raise();

        _previousAction.performed -= _ => _previousGameEvent.Raise();

        _selectSlotAction.performed -= ctx => _selectSlot.Raise(ctx.control.name);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            Sprint = true;
        else if (context.canceled)
            Sprint = false;
    }
}
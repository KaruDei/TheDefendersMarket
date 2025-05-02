using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Maps")]
    [SerializeField] private string _playerMapName = "Player";
    [SerializeField] private string _uiMapName = "UI";

    [Header("Events")]
    [SerializeField] private VoidGameEvent _attackGameEvent;
    [SerializeField] private BoolGameEvent _aimGameEvent;
    [SerializeField] private VoidGameEvent _interactGameEvent;
    [SerializeField] private VoidGameEvent _jumpGameEvent;
    [SerializeField] private VoidGameEvent _pauseGameEvent;
    [SerializeField] private VoidGameEvent _inventoryGameEvent;
    [SerializeField] private VoidGameEvent _previousGameEvent;
    [SerializeField] private VoidGameEvent _nextGameEvent;

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

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _sprintAction;

    public Vector2 Move {  get; private set; }
    public Vector2 Look {  get; private set; }
    public bool Sprint {  get; private set; }

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
    }

    private void Start()
    {
        //HideCursor();
    }

    private void OnEnable()
    {
        _currentMapName = string.IsNullOrEmpty(_currentMapName) ? _playerMapName : _currentMapName;
        _playerInput.SwitchCurrentActionMap(_currentMapName);
        BindActions();
    }

    private void OnDisable()
    {
        _currentMapName = _playerInput.currentActionMap.name;
        UnbindActions();
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

    //public static void HideCursor()
    //{
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    //public static void VisableCursor()
    //{
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.Confined;
    //}
}
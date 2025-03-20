using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;

        private InputActionMap _inputMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;
        private InputAction _inventoryAction;

        public Vector2 Move {  get; private set; }
        public Vector2 Look {  get; private set; }
        public bool Run {  get; private set; }
        public bool Jump {  get; private set; }
        public bool Attack {  get; private set; }
        public bool Inventory {  get; private set; }

        private void Awake()
        {
            _inputMap = _playerInput.currentActionMap;
            _moveAction = _inputMap.FindAction("Move");
            _lookAction = _inputMap.FindAction("Look");
            _runAction = _inputMap.FindAction("Run");
            _jumpAction = _inputMap.FindAction("Jump");
            _attackAction = _inputMap.FindAction("Attack");
            _inventoryAction = _inputMap.FindAction("Inventory");

            _moveAction.performed += OnMove;
            _lookAction.performed += OnLook;
            _runAction.performed += OnRun;
            _jumpAction.performed += OnJump;
            _attackAction.performed += OnAttack;
            _inventoryAction.performed += OnInventory;

            _moveAction.canceled += OnMove;
            _lookAction.canceled += OnLook;
            _runAction.canceled += OnRun;
            _jumpAction.canceled += OnJump;
            _attackAction.canceled += OnAttack;
            //_inventoryAction.canceled += OnInventory;
        }

        private void Start()
        {
            HideCursor();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValueAsButton();
        }
        
        private void OnAttack(InputAction.CallbackContext context)
        {
            Attack = context.ReadValueAsButton();
        }

        private void OnInventory(InputAction.CallbackContext context)
        {
            WindowsController.InventoryUIWindow();
        }

        public static void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void VisableCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void OnEnable()
        {
            _inputMap.Enable();
        }

        private void OnDisable()
        {
            _inputMap.Disable();
        }
    }
}
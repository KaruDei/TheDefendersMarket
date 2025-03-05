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

        public Vector2 Move {  get; private set; }
        public Vector2 Look {  get; private set; }
        public bool Run {  get; private set; }
        public bool Jump {  get; private set; }

        private void Awake()
        {
            _inputMap = _playerInput.currentActionMap;
            _moveAction = _inputMap.FindAction("Move");
            _lookAction = _inputMap.FindAction("Look");
            _runAction = _inputMap.FindAction("Run");
            _jumpAction = _inputMap.FindAction("Jump");

            _moveAction.performed += OnMove;
            _lookAction.performed += OnLook;
            _runAction.performed += OnRun;
            _jumpAction.performed += OnJump;

            _moveAction.canceled += OnMove;
            _lookAction.canceled += OnLook;
            _runAction.canceled += OnRun;
            _jumpAction.canceled += OnJump;
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

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
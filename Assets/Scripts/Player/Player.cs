using UnityEngine;

namespace Player
{
    [RequireComponent (typeof (MovementController))]
    [RequireComponent (typeof (InputManager))]
    public class Player : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private MovementController _movementController;
        [SerializeField] private InputManager _inputManager;

        public InputManager InputManager
        {
            get { return _inputManager; }
            private set { _inputManager = value; }
        }
    }
}

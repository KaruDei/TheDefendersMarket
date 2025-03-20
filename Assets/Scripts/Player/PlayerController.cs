using UnityEngine;

namespace Player
{
    [SelectionBase]
    [RequireComponent (typeof (MovementController), typeof(InputManager))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InputManager _inputManager;

        public InputManager InputManager => _inputManager;

        public bool IsCanMove = false;
    }
}
